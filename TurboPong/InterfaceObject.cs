using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace TurboPong
{
    internal class InterfaceObject : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Game game;

        public bool IsClickable = false;

        private StringBuilder interfaceText = new StringBuilder("DEFAULT TEXT");
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

        public int Width
        {
            get { return interfaceText.Length; }
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

        public InterfaceObject(Game game) : base(game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Initialize()
        {
            base.Initialize();
            selectedFont = SetFontSize(selectedFontSize);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(selectedFont, InterfaceText, new Vector2(PositionX, PositionY), TextColor);
            spriteBatch.End();
        }

        protected override void Dispose(bool disposing)
        {
            interfaceText.Clear();
            base.Dispose(disposing);
        }
    }
}
