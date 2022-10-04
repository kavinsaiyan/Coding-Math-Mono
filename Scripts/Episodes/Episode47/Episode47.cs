using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using CodingMath.InputSystem;
namespace CodingMath.Episodes
{
    public class Episode47 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly Random _random;
        private const float EMPTY_PERCENT = 0.5f;
        private const float GOLD_PERCENT = 0.3f;
        private const float TREASURE_CHEST_PERCENT = 0.16f;
        private const float GENIE_WISH_PERCENT = 0.04f;
        private readonly RewardItem[] _rewardItems;
        private readonly RewardItem[] _rewardItemsWholeNumberPercent;

        public Episode47()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _random = new Random();

            _rewardItems = new RewardItem[]
            {
                new RewardItem(EMPTY_PERCENT,"you get nothing!"),
                new RewardItem(GOLD_PERCENT,"you get ten gold!"),
                new RewardItem(TREASURE_CHEST_PERCENT,"you get treasure chest!"),
                new RewardItem(GENIE_WISH_PERCENT,"you get a genie wish!")
            };

            _rewardItemsWholeNumberPercent = new RewardItem[]
            {
                new RewardItem(5,"you get nothing!"),
                new RewardItem(4,"you get ten gold!"),
                new RewardItem(2,"you get treasure chest!"),
                new RewardItem(1,"you get a genie wish!")
            };
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                Roll2();
            }
        }

        private void Roll()
        {
            float random = _random.NextSingle(0, 1);

            if (random < EMPTY_PERCENT)
            {
                Console.WriteLine("you get nothing!");
                return;
            }
            random -= EMPTY_PERCENT;

            if (random < GOLD_PERCENT)
            {
                Console.WriteLine("you get ten gold!");
                return;
            }
            random -= GOLD_PERCENT;


            if (random < TREASURE_CHEST_PERCENT)
            {
                Console.WriteLine("you get treasure chest!");
                return;
            }
            random -= TREASURE_CHEST_PERCENT;


            if (random < GENIE_WISH_PERCENT)
            {
                Console.WriteLine("you get a genie wish!");
                return;
            }
            random -= GENIE_WISH_PERCENT;
        }

        private void Roll1()
        {
            float random = _random.NextSingle(0, 1);

            for (int i = 0; i < _rewardItems.Length; i++)
            {
                if (random < _rewardItems[i].Percent)
                {
                    Console.WriteLine(_rewardItems[i].Reward);
                    break;
                }
                random -= _rewardItems[i].Percent;
            }
        }

        private void Roll2()
        {
            float random = _random.NextSingle(0, 1);

            float total = 0;
            for (int i = 0; i < _rewardItemsWholeNumberPercent.Length; i++)
                total += _rewardItemsWholeNumberPercent[i].Percent;

            random *= total;

            for (int i = 0; i < _rewardItemsWholeNumberPercent.Length; i++)
            {
                if (random < _rewardItemsWholeNumberPercent[i].Percent)
                {
                    Console.WriteLine(_rewardItemsWholeNumberPercent[i].Reward);
                    break;
                }
                random -= _rewardItemsWholeNumberPercent[i].Percent;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        class RewardItem
        {
            private readonly float _percent;
            private readonly string _reward;

            public RewardItem(float percent, string reward)
            {
                _percent = percent;
                _reward = reward;
            }

            public float Percent { get => _percent; }
            public string Reward { get => _reward; }
        }
    }
}