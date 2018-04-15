using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.App;

namespace Assets.Source.UI
{
    public class ShopItemUI : MonoBehaviour
    {

        public void PurchaseKickForceUpgrade()
        {
            Singletons.playerProfile.KickForce += 200f;
        }

        public void PurchaseKickCountUpgrade()
        {
            int kickCount = Singletons.playerProfile.KickCount;
            Singletons.playerProfile.KickCount = kickCount + 2;
        }

        public void PurchaseStatsReset()
        {
            Singletons.playerProfile.ResetStats();
        }
    }
}
