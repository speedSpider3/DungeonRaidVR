﻿using UnityEngine;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.Characters.Bosses;
using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "TargetBoss", menuName = AbilityMenuPrefix + "Target Boss")]
	public class TargetBossAbility : Ability {
		protected override bool TargetCast(Effect[] effects) {
			if (!(Owner is Hero)) {
				return false;
			}

			Boss boss = (Owner as Hero).TargetCharacter as Boss;
			if (boss == null) {
				return false;
			}

			foreach (Effect effect in effects) {
				if (effect.ApplyToCaster) {
					effect.Apply(boss, Owner, TargetPoint);
				} else {
					effect.Apply(boss, boss, TargetPoint);
				}
			}

			return true;
		}
	}
}