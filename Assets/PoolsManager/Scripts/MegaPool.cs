using System.Collections.Generic;
using UnityEngine;

namespace PoolsManagement {

    public class MegaPool : MonoBehaviour {

        public List<PoolSettingsSO> initialPools;

        Dictionary<string, Pool> dictionary = new Dictionary<string, Pool>();

        private void OnEnable() {
            if (initialPools != null && initialPools.Count > 0) {
                foreach (var item in initialPools) {
                    GetPoolPart(item);
                }
            }
        }

        public Pool GetPoolPart(PoolSettingsSO poolSettings) {
            Pool poolPart = GetPoolPart(poolSettings.key);
            if (poolPart == null) {
                poolPart = CreateNewPoolPart(poolSettings);
            }
            return poolPart;
        }

        public Pool GetPoolPart(string key) {
            Pool poolPart = null;
            dictionary.TryGetValue(key, out poolPart);
            return poolPart;
        }

        Pool CreateNewPoolPart(PoolSettingsSO poolSet) {
            Transform newHolder = new GameObject("Holder_" + poolSet.key).transform;
            newHolder.SetParent(transform);
            Pool newPoolPart = new Pool(this, poolSet, newHolder);
            dictionary.Add(poolSet.key, newPoolPart);
            if (poolSet.GetPool() == null) {
                poolSet.SetPool(newPoolPart);
            }
            return newPoolPart;
        }

        private void OnDestroy() {
            if (PoolSettingsSO.generalMegaPool == this) {
                PoolSettingsSO.SetGeneralMegaPool(null);
            }
            foreach (var item in dictionary) {
                if (item.Value == item.Value.poolSet.GetPool()) {
                    item.Value.poolSet.SetPool(null);
                }
            }
        }

    }
}