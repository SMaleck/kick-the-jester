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
            App.Cache.playerProfile.KickForce += 200f;
        }

        public void PurchaseKickCountUpgrade()
        {
            int kickCount = App.Cache.playerProfile.KickCount;
            App.Cache.playerProfile.KickCount = kickCount + 2;
        }

        public void PurchaseStatsReset()
        {
            App.Cache.playerProfile.ResetStats();
        }
    }
}
