﻿using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Abilities;

namespace DungeonRaid.Characters.Bosses.Interactables {
	[RequireComponent(typeof(Collider))]
	public class ChannelableTriggerVolume : Interactable {
		[SerializeField] private Cost activationCost = new Cost();
		[SerializeField] private Cost channelCost = new Cost();
		[SerializeField] private ChannelableBehaviour[] behaviours = null;

		private bool channeling = false;
		private Boss boss;
		private GameObject triggeringObject;

		private const float fixedChannelTimer = 1;
		private float channelTimer = 0;

		private void OnTriggerEnter(Collider other) {
			if (triggeringObject == null && other.CompareTag("Boss")) {
				if (boss == null) {
					boss = other.GetComponentInParent<Boss>();
				}

				channelTimer = 0;
				triggeringObject = other.gameObject;

				if (boss.PayCost(activationCost)) {
					channeling = true;
					foreach (ChannelableBehaviour effect in behaviours) {
						effect.BeginChannel();
					}
				}
			}
		}

		private void OnTriggerStay(Collider other) {
			if (channeling) {
				channelTimer += Time.deltaTime;
				if (channelTimer >= fixedChannelTimer) {
					channelTimer -= fixedChannelTimer;

					if (other.gameObject == triggeringObject) {
						if (boss.PayCost(channelCost)) {
							foreach (ChannelableBehaviour effect in behaviours) {
								effect.ChannelUpdate();
							}
						} else {
							OnTriggerExit(other);
						}
					}
				}
			}

		}

		private void OnTriggerExit(Collider other) {
			if (other.gameObject == triggeringObject) {
				channeling = false;
				triggeringObject = null;
				foreach (ChannelableBehaviour effect in behaviours) {
					effect.EndChannel();
				}
			}
		}
	}
}