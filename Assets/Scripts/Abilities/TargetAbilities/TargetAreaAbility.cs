﻿using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetArea", menuName = AbilityMenuPrefix + "Target Area")]
	public class TargetAreaAbility : Ability {
		[SerializeField, Min(0.01f)] private float radius = 1;
		[SerializeField] private bool hitSelf = false;
		[SerializeField] private LayerMask targetLayers = 1;

		protected override bool TargetCast(Effect[] effects) {
			Collider[] overlaps = Physics.OverlapSphere(TargetPoint, radius, targetLayers);

			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(Owner, Owner, TargetPoint);
				} else {
					foreach (Collider col in overlaps) {
						Character target = col.GetComponent<Character>();
						if (target != null) {
							if (hitSelf || target != Owner) {
								effect.Apply(target, target, TargetPoint);
							}
						}
					}
				}
			}

			return true;
		}
	}
}
