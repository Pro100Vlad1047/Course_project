using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]public float Speed = 2;
    private Rigidbody2D _rb2D;

    private void Start() {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        _rb2D.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) _rb2D.velocity += Vector2.left * Speed;
        if (Input.GetKey(KeyCode.RightArrow)) _rb2D.velocity += Vector2.right * Speed;
        if (Input.GetKey(KeyCode.UpArrow)) _rb2D.velocity += Vector2.up * Speed;
        if (Input.GetKey(KeyCode.DownArrow)) _rb2D.velocity += Vector2.down * Speed;
    }

}
