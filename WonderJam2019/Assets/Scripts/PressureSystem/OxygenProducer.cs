using System.Collections.Generic;
using UnityEngine;

public class OxygenProducer : MonoBehaviour
{
    public long AvailablePressure { get; set; }
    public IEnumerable<OxygenController> OxygenControllers { get; set; }
}