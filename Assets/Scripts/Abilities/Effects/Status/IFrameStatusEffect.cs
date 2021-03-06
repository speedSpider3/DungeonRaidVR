﻿using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "IFrames", menuName = StatusEffectMenuPrefix + "Invincibility")]
	public class IFrameStatusEffect : StatusEffect {
		protected override void StartEffect(Character target) {
			target.FindMeter("Health").IsLocked = true;
		}

		protected override void StopEffect(Character target) {
			target.FindMeter("Health").IsLocked = false;
		}
	}
}
