﻿/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using TMPro;
using UnityEngine;

/**
 * Our car will track a reticle and collide with a <see cref="PackageBehaviour"/>.
 */
public class CarBehaviour : MonoBehaviour
{
    public ReticleBehaviour Reticle;
    public float Speed = 1.2f;
    private TextMeshProUGUI _scoreText;
    private int _counter;
    private AudioSource _audio;
    [SerializeField] private ParticleSystem particleSystem;
    private void Start()
    {
        _scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var trackingPosition = Reticle.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position =
            Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        if (Package != null)
        {
            var ps = Instantiate(particleSystem, transform.position, transform.rotation);
            ps.Play();
            Destroy(other.gameObject);
            _counter++;
            _audio.Play();
            _scoreText.text = $"Score: {_counter.ToString()}";
        }
    }
}
