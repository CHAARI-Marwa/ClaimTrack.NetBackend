﻿using ClaimTrack.NetBackend.Dto;
using ClaimTrack.NetBackend.Models;
using ClaimTrack.NetBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClaimTrack.NetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleVenduController : ControllerBase
    {
        private readonly IArticleVenduRepository _repository;

        public ArticleVenduController(IArticleVenduRepository repository)
        {
            _repository = repository;
        }

        // GET : Obtenir tous les articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles()
        {
            var articles = await _repository.GetArticlesAsync();
            var articleDtos = articles.Select(article => new ArticleDto
            {
                Id = article.Id,
                NomArticle = article.NomArticle,
                IdUser = article.IdUser,
                DateAchat = article.DateAchat,
                DureeGarantie = article.DureeGarantie
            });

            return Ok(articleDtos);  // Retourne la liste des DTOs
        }

        // GET : Obtenir un article par son Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticleById(int id)
        {
            var article = await _repository.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();  // Si l'article n'est pas trouvé
            }

            var articleDto = new ArticleDto
            {
                Id = article.Id,
                NomArticle = article.NomArticle,
                IdUser = article.IdUser,
                DateAchat = article.DateAchat,
                DureeGarantie = article.DureeGarantie
            };

            return Ok(articleDto);  // Retourne le DTO de l'article
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesByUserId(int userId)
        {
            var articles = await _repository.GetArticlesByUserIdAsync(userId);

            if (articles == null || !articles.Any())
            {
                return NotFound();  // Si aucun article n'est trouvé pour cet utilisateur
            }

            var articleDtos = articles.Select(article => new ArticleDto
            {
                Id = article.Id,
                NomArticle = article.NomArticle,
                IdUser = article.IdUser,
                DateAchat = article.DateAchat,
                DureeGarantie = article.DureeGarantie
            }).ToList();

            return Ok(articleDtos);  // Retourne la liste des DTOs des articles
        }

        // POST : Ajouter un nouvel article
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> AddArticle(ArticleDto articleDto)
        {
            var article = new ArticleVendu
            {
                NomArticle = articleDto.NomArticle,
                IdUser = articleDto.IdUser,
                DateAchat = articleDto.DateAchat,
                DureeGarantie = articleDto.DureeGarantie
            };

            var addedArticle = await _repository.AddArticleAsync(article);

            var addedArticleDto = new ArticleDto
            {
                Id = addedArticle.Id,
                NomArticle = addedArticle.NomArticle,
                IdUser = addedArticle.IdUser,
                DateAchat = addedArticle.DateAchat,
                DureeGarantie = addedArticle.DureeGarantie
            };

            return CreatedAtAction(nameof(GetArticleById), new { id = addedArticleDto.Id }, addedArticleDto);
        }
    }
}
