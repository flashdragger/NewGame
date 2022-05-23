using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FSM {
    public struct ReactType {
        public Attributes.Elements ReactEle;
        public float ReactRate;

        public ReactType(Attributes.Elements ReactEle, float ReactRate)
        {
            this.ReactEle = ReactEle;
            this.ReactRate = ReactRate;
        }
    }
    public class EleReaction {
        private Dictionary<Attributes.Elements, List<ReactType>> _reactMap;

        public EleReaction() {
            _reactMap = new Dictionary<Attributes.Elements, List<ReactType>>();
            _reactMap.Add(Attributes.Elements.water, new List<ReactType> {new ReactType(Attributes.Elements.fire, 2.0f), new ReactType(Attributes.Elements.ice, 1.0f)});
            _reactMap.Add(Attributes.Elements.fire, new List<ReactType> {new ReactType(Attributes.Elements.water, 1.5f), new ReactType(Attributes.Elements.ice, 2.0f)});
            _reactMap.Add(Attributes.Elements.ice, new List<ReactType>{new ReactType(Attributes.Elements.fire, 1.5f), new ReactType(Attributes.Elements.water, 1.0f)});
        }

        public float Reaction(Attributes.Elements oldEle, Attributes.Elements newEle) {
            if (oldEle != newEle) {
                foreach (ReactType item in _reactMap[oldEle]) {
                    if (newEle == item.ReactEle) {
                        return item.ReactRate;
                    }
                }
            }
            return 1.0f;
        }
    }
}