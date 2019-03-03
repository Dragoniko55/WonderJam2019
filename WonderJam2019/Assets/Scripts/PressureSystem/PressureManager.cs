using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressureManager : MonoBehaviour
{
    private List<PressureGraph> _networks { get; } = new List<PressureGraph>();

    private OxygenProducer[] _producers;
    private Dictionary<OxygenConsumer, HashSet<OxygenController>> _consumersToControllers { get; } = new Dictionary<OxygenConsumer, HashSet<OxygenController>>();
    private Dictionary<OxygenController, HashSet<OxygenProducer>> _controllersToProducers { get; } = new Dictionary<OxygenController, HashSet<OxygenProducer>>();

    public event Action<int> GeneratedGraphs;
    public int NetworkCount => this._networks.Where(n => n.Consumers.Any()).Count();

    public int GetNetworkIndex(OxygenConsumer consumer)
    {
        return this._networks
            .Select((obj, index) => new { obj, index })
            .FirstOrDefault(oi => oi.obj.Contains(consumer))
            ?.index ?? -1;
    }

    public IEnumerable<OxygenController> GetLinkedControllers(OxygenConsumer oxygenConsumer)
    {
        return this._consumersToControllers
            .TryGetValue(oxygenConsumer, out var controllers) ? 
                controllers : 
                Enumerable.Empty<OxygenController>();
    }

    private void Start()
    {
        // Find the producers.
        this._producers = FindObjectsOfType<OxygenProducer>();
        Debug.Log($"Found {this._producers.Length} oxygen producers.");

        // Find the controllers.
        var controllers = FindObjectsOfType<OxygenController>();
        Debug.Log($"Found {controllers.Length} oxygen controllers.");

        // Create a reverse lookup for controllers.
        foreach (var controller in controllers)
        {
            foreach (var consumer in controller.OxygenConsumers)
            {
                if (!this._consumersToControllers.ContainsKey(consumer))
                {
                    this._consumersToControllers[consumer] = new HashSet<OxygenController>();
                }

                this._consumersToControllers[consumer].Add(controller);
            }
        }
        Debug.Log($"Mapped {this._consumersToControllers.Keys.Count} oxygen consumers to their associated oxygen controllers.");

        // Create a revers lookup for producers.
        foreach (var producer in FindObjectsOfType<OxygenProducer>())
        {
            foreach (var controller in producer.OxygenControllers)
            {
                if (!this._controllersToProducers.ContainsKey(controller))
                {
                    this._controllersToProducers[controller] = new HashSet<OxygenProducer>();
                }

                this._controllersToProducers[controller].Add(producer);
            }
        }
        Debug.Log($"Mapped {this._controllersToProducers.Keys.Count} oxygen controllers to their associated oxygen producers.");

        // Build the initial networks and pressurise them.
        this.BuildNetworks();

        // Register for events.
        foreach (var controller in controllers)
        {
            controller.Opened += this.Controller_StateChanged;
        }
    }

    private void Controller_StateChanged(OxygenController controller)
    {
        this.RebuildNetworks();
    }

    private void Producer_Activated(OxygenProducer obj)
    {
        this._networks.FirstOrDefault(n => n.Producers.Contains(obj))?.UpdatePressureLevels();
    }

    private void Consumer_VolumeChanged(OxygenConsumer consumer)
    {
        this._networks.FirstOrDefault(n => n.Contains(consumer))?.UpdatePressureLevels();
    }

    private void RebuildNetworks()
    {
        // Unbind events.
        foreach (var network in this._networks)
        {
            network.UnbindEvents();
            network.ResetPressureLevels();
        }

        // Rebuild the network graphs.
        this._networks.Clear();
        this.BuildNetworks();
    }

    private void BuildNetworks()
    {
        // Build the individual network graphs.
        var mappedProducers = new HashSet<OxygenProducer>();
        foreach (var producer in this._producers)
        {
            if (!mappedProducers.Contains(producer))
            {
                var newGraph = new PressureGraph(this, producer);
                foreach (var mappedProducer in newGraph.Producers)
                {
                    mappedProducers.Add(mappedProducer);
                }

                this._networks.Add(newGraph);

                producer.Activated += this.Producer_Activated;
            }
        }

        Debug.Log(
            $"Built the graph. {this._networks.Count} networks including " +
            $"{this._networks.SelectMany(n => n.Consumers).Count()} consumers, " +
            $"{this._networks.SelectMany(n => n.Controllers).Count()} controllers, " +
            $"{this._networks.SelectMany(n => n.Producers).Count()} producers.");

        // Update the pressure in each netwok.
        this.UpdatePressureLevels();

        if(this.GeneratedGraphs != null)
            this.GeneratedGraphs(this.NetworkCount);
    }

    private void UpdatePressureLevels()
    {
        foreach (var network in this._networks)
        {
            network.UpdatePressureLevels();
        }
    }

    private class PressureGraph
    {
        private readonly PressureManager _owner;
        private HashSet<OxygenProducer> _producers { get; } = new HashSet<OxygenProducer>();
        private Dictionary<OxygenController, HashSet<OxygenController>> _relationships { get; } = new Dictionary<OxygenController, HashSet<OxygenController>>();

        public IEnumerable<OxygenProducer> Producers => this._producers;
        public IEnumerable<OxygenController> Controllers => this._relationships.Keys;
        public IEnumerable<OxygenConsumer> Consumers => this._relationships.Keys.SelectMany(c => c.OxygenConsumers).Distinct();

        public PressureGraph(PressureManager owner, OxygenProducer initialProducer)
        {
            this._owner = owner;
            this.BuildGraph(initialProducer);
            this.BindEvents();
        }

        public bool Contains(OxygenConsumer consumer)
        {
            return this._owner._consumersToControllers[consumer]
                .Any(c => this._relationships.ContainsKey(c));
        }

        public void UpdatePressureLevels()
        {
            var consumers = this.Consumers.ToArray();
            if (consumers.Any())
            {
                var totalPressure = this._producers.Sum(p => p.CurrentPressure);
                var individualPressure = totalPressure / consumers.Length;
                
                foreach (var consumer in consumers)
                {
                    consumer.CurrentPressure = individualPressure;
                }
            }
        }

        public void ResetPressureLevels()
        {
            var consumers = this.Consumers.ToArray();
            foreach (var consumer in consumers)
            {
                consumer.CurrentPressure = 0;
            }
        }

        public void UnbindEvents()
        {
            foreach (var consumer in this.Consumers)
            {
                consumer.VolumeChanged -= this._owner.Consumer_VolumeChanged;
            }

            foreach (var controller in this.Controllers)
            {
                controller.Closed -= this._owner.Controller_StateChanged;
            }
        }

        private void BindEvents()
        {
            foreach (var consumer in this.Consumers)
            {
                consumer.VolumeChanged += this._owner.Consumer_VolumeChanged;
            }

            foreach (var controller in this.Controllers)
            {
                controller.Closed += this._owner.Controller_StateChanged;
            }
        }

        private void BuildGraph(OxygenProducer initialProducer)
        {
            var producers = new Queue<OxygenProducer>(new[] { initialProducer });
            while (producers.Count > 0)
            {
                var producer = producers.Dequeue();

                foreach (var controller in producer.OxygenControllers)
                {
                    foreach (var newProducer in this.MapController(controller))
                    {
                        producers.Enqueue(newProducer);
                    }
                }

                this._producers.Add(producer);
            }
        }

        private IEnumerable<OxygenProducer> MapController(OxygenController oxygenController)
        {
            IEnumerable<OxygenProducer> returnValue = Enumerable.Empty<OxygenProducer>();

            if (oxygenController.IsOpened)
            {
                if (!this._relationships.ContainsKey(oxygenController))
                {
                    if (this._owner._controllersToProducers.ContainsKey(oxygenController))
                    {
                        returnValue = returnValue.Union(this._owner._controllersToProducers[oxygenController]);
                    }

                    this._relationships[oxygenController] = new HashSet<OxygenController>();
                    foreach (var consumer in oxygenController.OxygenConsumers)
                    {
                        foreach (var controller in this._owner._consumersToControllers[consumer])
                        {
                            returnValue = returnValue.Union(this.MapController(controller));
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}