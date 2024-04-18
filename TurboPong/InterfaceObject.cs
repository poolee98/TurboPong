using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Text;

namespace TurboPong
{
    delegate void OnClickHandler();
    delegate void OnHover();

    internal class InterfaceObject
    {
        private new Game1 game;

        private MouseState mouseState;

        public bool IsClickable = false;
        public event OnClickHandler OnClick;
        public event OnHover OnHover;
        public bool ShadowIfHoveredOver = false;
        private bool hoveredOver = false;

        private StringBuilder interfaceText = new StringBuilder("DEFAULT TEXT");

        public Rectangle Bounds;

        public string InterfaceText
        {
            set 
            {
                interfaceText.Clear();
                interfaceText.Append(value);
            }
            get 
            { 
                return interfaceText.ToString(); 
            }
        }

        public int PositionX = 1;
        public int PositionY = 1;

        public Color TextColor = Color.White;

        private SpriteFont selectedFont;
        private FontSize selectedFontSize = FontSize.Small;
        public FontSize InterfaceFontSize
        {
            get { return selectedFontSize; }
            set { selectedFont = SetFontSize(value); }     
        }

        public enum FontSize
        {
            Small,
            Medium,
            Large,
            Giga
        }

        public Size2 Size
        {
            get 
            {
                return selectedFont.MeasureString(InterfaceText).ToSize();
            }
        }

        private SpriteFont SetFontSize(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Medium:
                    selectedFontSize = FontSize.Medium;
                    return selectedFont = game.Content.Load<SpriteFont>("ARCADECLASSIC_Medium");
                case FontSize.Large:
                    selectedFontSize = FontSize.Large;
                    return selectedFont = game.Content.Load<SpriteFont>("ARCADECLASSIC_Large");
                case FontSize.Giga:
                    selectedFontSize = FontSize.Giga;
                    return selectedFont = game.Content.Load<SpriteFont>("ARCADECLASSIC_Giga");
                default:
                    selectedFontSize = FontSize.Small;
                    return selectedFont = game.Content.Load<SpriteFont>("ARCADECLASSIC_Small");
            }

        }

        public InterfaceObject(Game game) 
        {
            this.game = (Game1)game;
            selectedFont = SetFontSize(selectedFontSize);
            Bounds = new Rectangle(PositionX, PositionY, (int)Size.Width, (int)Size.Height);
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (IsClickable)
            {
                if (mouseState.Position.Y >= PositionY && mouseState.Position.Y <= PositionY + Size.Height)
                {
                    if (mouseState.Position.X >= PositionX && mouseState.Position.X <= PositionX + Size.Width)
                    {
                        hoveredOver = true;
                        // Fire OnHover event
                        if (OnHover != null)
                        {
                            OnHover();
                        }
                        // Fire OnClick event
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (OnClick != null)
                            {
                                OnClick();
                            }
                        }
                    }
                }
                else
                {
                    hoveredOver = false;
                }
            }

        }
        public void Draw(GameTime gameTime)
        {
            if (ShadowIfHoveredOver && hoveredOver)
            {
                game.spriteBatch.DrawString(selectedFont, InterfaceText, new Vector2(PositionX + 5, PositionY + 5), Color.Black);
            }
            game.spriteBatch.DrawString(selectedFont, InterfaceText, new Vector2(PositionX, PositionY), TextColor);
        }
    }
}
