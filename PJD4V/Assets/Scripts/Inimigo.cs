using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField] private Transform alvo;

    [SerializeField] private float velocidademov;

    [SerializeField] private Rigidbody2D rigidibody;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float distancialoka;

    [SerializeField] private Animator animator;

    [SerializeField] private float raiovisao;

    [SerializeField] private LayerMask layerareavisao;


    // Update is called once per frame
    void Update()
    {
        ProcurarJogador();
        if(this.alvo != null)
        {
            Mover();
        }
        else
        {
            ParadeMover();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.raiovisao);
    }

    private void ProcurarJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raiovisao, this.layerareavisao);
        if (colisor != null)
        {
            this.alvo = colisor.transform;
        }
        else
        {
            this.alvo = null;                 
        }
    }
    
    private void Mover()
    {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;
        float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);
        if(distancia >= this.distancialoka)
        {
            Vector2 direcao = posicaoAlvo - posicaoAtual;
            direcao = direcao.normalized;

            this.rigidibody.velocity = (this.velocidademov * direcao);
            if(this.rigidibody.velocity.x > 0)
            {
                this.spriteRenderer.flipX = false;
            }
            else if(this.rigidibody.velocity.x < 0 )
            {
                this.spriteRenderer.flipX = true;
            }
            this.animator.SetBool("movendo", true);
        }
        else
        {
            ParadeMover();
        }
    }
    private void ParadeMover()
    {
        this.rigidibody.velocity = Vector2.zero;
        this.animator.SetBool("movendo", false);
    }
}
