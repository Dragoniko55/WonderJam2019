using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressureManager : MonoBehaviour
{
    private List<PressureGraph> _networks { get; } = new List<PressureGraph>();

    private Dictionary<OxygenConsumer, HashSet<OxygenController>> _consumersToControllers { get; } = new Dictionary<OxygenConsumer, HashSet<OxygenController>>();
    private Dictionary<OxygenController, HashSet<OxygenProducer>> _controllersToProducers { get; } = new Dictionary<OxygenController, HashSet<OxygenProducer>>();

    public int GetNetworkIndex(OxygenConsumer consumer)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OxygenController> GetLinkedControllers(OxygenProducer oxygenProducer)
    {
        return this._networks
            .FirstOrDefault(n => n.Contains(oxygenProducer))
            ?.Controllers
            ?? Enumerable.Empty<OxygenController>();
    }

    private void Start()
    {
        Debug.Log("Started building the graph.");

        var controllers = FindObjectsOfType<OxygenController>();
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

        // Map the producers.
        var mappedProducers = new HashSet<OxygenProducer>();
        foreach (var producer in FindObjectsOfType<OxygenProducer>())
        {
            if (!mappedProducers.Contains(producer))
            {
                var newGraph = new PressureGraph(this, producer);
                foreach (var mappedProducer in newGraph.Producers)
                {
                    mappedProducers.Add(mappedProducer);
                }

                this._networks.Add(newGraph);
            }
        }

        // Register for events.
        foreach (var controller in controllers)
        {
            controller.Opened += this.Controller_Opened;
        }

        Debug.Log("Built the graph.");
        Debug.Log($"{this._networks.Count} networks, {this._networks.SelectMany(n => n.Consumers).Count()} consumers, {this._networks.SelectMany(n => n.Controllers).Count()} controllers, {this._networks.SelectMany(n => n.Producers).Count()} producers.");
    }

    private void Controller_Opened(OxygenController controller)
    {
        throw new NotImplementedException();
    }

    private void Controller_Closed(OxygenController controller)
    {
        var network = this._networks.FirstOrDefault(n => n.Contains(controller));
        if (network != null)
        {

        }

        if (!this._networks.Any(n => n.Contains(controller)))
        {
            controller.Closed -= this.Controller_Closed;
        }
    }

    private void Consumer_VolumeChanged(OxygenConsumer consumer)
    {
        throw new NotImplementedException();
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
        }

        public void Merge(PressureGraph other, OxygenController oxygenController)
        {
            throw new NotImplementedException();
        }

        public PressureGraph Split(OxygenController oxygenController)
        {
            throw new NotImplementedException();
        }

        public bool Contains(OxygenController controller)
        {
            return this._relationships.ContainsKey(controller);
        }

        public bool Contains(OxygenProducer producer)
        {
            return this._producers.Contains(producer);
        }

        private void BuildGraph(OxygenProducer initialProducer)
        {
            var producers = new Queue<OxygenProducer>(new[] { initialProducer });
            var mappedControllers = new HashSet<OxygenController>();

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

                        consumer.VolumeChanged += this._owner.Consumer_VolumeChanged;
                    }

                    oxygenController.Closed += this._owner.Controller_Closed;
                }
            }

            return returnValue;
        }
    }
}