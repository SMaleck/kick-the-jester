using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.App;

namespace Assets.Source.UI
{
    public class ShopItemUI : MonoBehaviour
    {

        public void onKickForcePurchase()
        {
            Singletons.playerProfile.KickForce = 1000f;
        }
    }
}
