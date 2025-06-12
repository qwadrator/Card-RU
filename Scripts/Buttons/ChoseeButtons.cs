using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseeButtons : MonoBehaviour
{
    public Text HeroText;
    public Text MaxHp;
    public Image Hero;
    public Sprite EnemySprite;
    public Image Artifact;
    public Sprite SpriteHero;
    public List<AbstractGameCharacter> GameCharacters;
    public void Start()
    {
        GameCharacters = new List<AbstractGameCharacter>();
        GameCharacters.Add(new Hero1());
        GameCharacters.Add(new Hero2());
        Hero1Ckilck();
    }
    public void Hero1Ckilck()
    {
        HeroText.text = GameCharacters[0].Description;
        MaxHp.text = GameCharacters[0].MAXHP.ToString();
        Hero.sprite = SpriteHero;
        SelectedGameCharacter.Hero = GameCharacters[0];
        SelectedGameCharacter.HeroSprite = SpriteHero;
        Enemies.Enemy = new Monster1();
        Enemies.EnemySprite = EnemySprite;
    }
    public void Hero2Ckilck()
    {
        HeroText.text = GameCharacters[1].Description;
        MaxHp.text = GameCharacters[1].MAXHP.ToString();
        Hero.sprite = SpriteHero;
        SelectedGameCharacter.Hero = GameCharacters[1];
        SelectedGameCharacter.HeroSprite = SpriteHero;
        Enemies.Enemy = new Monster1();
        Enemies.EnemySprite = EnemySprite;
    }
}
