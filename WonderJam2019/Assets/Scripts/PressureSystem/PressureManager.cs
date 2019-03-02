using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressureManager : MonoBehaviour
{
    private List<PressureGraph> _networks { get; }

    private Dictionary<OxygenConsumer, HashSet<OxygenController>> _consumersToControllers { get; } = new Dictionary<OxygenConsumer, HashSet<OxygenController>>();
    private Dictionary<OxygenController, HashSet<OxygenProducer>> _controllersToProducers { get; } = new Dictionary<OxygenController, HashSet<OxygenProducer>>();

    private void Start()
    {
        // Create a reverse lookup for controllers.
        foreach (var controller in FindObjectsOfType<OxygenController>())
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

    }

    private class PressureGraph
    {
        private readonly PressureManager _owner;
        private HashSet<OxygenProducer> _producers { get; } = new HashSet<OxygenProducer>();
        private Dictionary<OxygenController, HashSet<OxygenController>> _relationships { get; } = new Dictionary<OxygenController, HashSet<OxygenController>>();

        public IEnumerable<OxygenProducer> Producers => this._producers;
        public IEnumerable<OxygenController> Controllers => this._relationships.Keys;
        public IEnumerable<OxygenConsumer> Consumers => this._relationships.Keys.SelectMany(c => c.OxygenConsumers);

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

        private void BuildGraph(OxygenProducer initialProducer)
        {
            var producers = new Queue<OxygenProducer>(new[] { initialProducer });
            var mappedControllers = new HashSet<OxygenController>();

            foreach (var producer in producers)
            {
                foreach (var controller in initialProducer.OxygenControllers)
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

            if (!this._relationships.ContainsKey(oxygenController))
            {
                this._relationships[oxygenController] = new HashSet<OxygenController>();
                foreach (var consumer in oxygenController.OxygenConsumers)
                {
                    foreach (var controller in this._owner._consumersToControllers[consumer])
                    {
                        returnValue = returnValue.Union(this.MapController(controller));
                    }
                }
            }

            return returnValue;
        }
    }
}