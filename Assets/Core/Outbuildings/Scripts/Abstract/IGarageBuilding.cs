using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGarageBuilding
{
    public IVenicle SetFreeVenicleTarget(IProductiveBuilding target);
    public bool ContatainsFreeVenicle();
}
