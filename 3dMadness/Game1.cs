using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3dMadness
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;

        // Cube definition
        VertexPositionTexture[][] vertices;
        VertexBuffer[] vertexBuffer;
        BasicEffect[] effect;
        Texture2D[] texture;

        Matrix worldRotation;
        Matrix worldTranslation;

        Vector3 frontTopLeft = new Vector3(-1, 1, 1);
        Vector3 frontTopRight = new Vector3(1, 1, 1);
        Vector3 frontBottomLeft = new Vector3(-1, -1, 1);
        Vector3 frontBottomRight = new Vector3(1, -1, 1);
        Vector3 backTopLeft = new Vector3(-1, 1, -1);
        Vector3 backTopRight = new Vector3(1, 1, -1);
        Vector3 backBottomLeft = new Vector3(-1, -1, -1);
        Vector3 backBottomRight = new Vector3(1, -1, -1);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(this, new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            Components.Add(camera);

            // Initialize vertices
            vertices = new VertexPositionTexture[6][];
            vertexBuffer = new VertexBuffer[6];
            effect = new BasicEffect[6];
            texture = new Texture2D[6];

            for (int i = 0; i < 6; i++)
            { 
                vertices[i] = new VertexPositionTexture[4];

                switch (i)
                {
                    case 0: // Front face vertices                                    
                        vertices[i][0] = new VertexPositionTexture(frontTopLeft,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(frontTopRight,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(frontBottomLeft,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(frontBottomRight,
                            new Vector2(1f, 1f));
                        break;
                    case 1: // Top face vertices            
                        vertices[i][0] = new VertexPositionTexture(backTopLeft,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(backTopRight,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(frontTopLeft,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(frontTopRight,
                            new Vector2(1f, 1f));
                        break;
                    case 2: // Back face vertices            
                        vertices[i][0] = new VertexPositionTexture(backTopRight,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(backTopLeft,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(backBottomRight,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(backBottomLeft,
                            new Vector2(1f, 1f));
                        break;
                    case 3: // Left face vertices            
                        vertices[i][0] = new VertexPositionTexture(backTopLeft,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(frontTopLeft,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(backBottomLeft,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(frontBottomLeft,
                            new Vector2(1f, 1f));
                        break;
                    case 4: // Bottom face vertices            
                        vertices[i][0] = new VertexPositionTexture(frontBottomLeft,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(frontBottomRight,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(backBottomLeft,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(backBottomRight,
                            new Vector2(1f, 1f));
                        break;
                    case 5: // Right face vertices            
                        vertices[i][0] = new VertexPositionTexture(frontTopRight,
                            new Vector2(0f, 0f));
                        vertices[i][1] = new VertexPositionTexture(backTopRight,
                            new Vector2(1f, 0f));
                        vertices[i][2] = new VertexPositionTexture(frontBottomRight,
                            new Vector2(0f, 1f));
                        vertices[i][3] = new VertexPositionTexture(backBottomRight,
                            new Vector2(1f, 1f));                        
                        break;
                }            

                vertexBuffer[i] = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture),
                    vertices[i].Length, BufferUsage.None);
                vertexBuffer[i].SetData(vertices[i]);
                effect[i] = new BasicEffect(GraphicsDevice);
                texture[i] = Content.Load<Texture2D>($@"Textures/trees{i + 1}");
            }                           

            /* Using VertexPositionColor
            vertices[0] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Blue);
            vertices[1] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Green);
            vertices[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Yellow);
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture),
                vertices.Length, BufferUsage.None);
            */

            worldRotation = Matrix.Identity;
            worldTranslation = Matrix.Identity;            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Translate the matrix
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                worldTranslation *= Matrix.CreateTranslation(-0.01f, 0f, 0f);
            if (keyboardState.IsKeyDown(Keys.Right))
                worldTranslation *= Matrix.CreateTranslation(0.01f, 0f, 0f);

            // Rotate the matrix
            //worldRotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 60); // Rotate around Y
            worldRotation *= Matrix.CreateFromYawPitchRoll(MathHelper.PiOver4 / 60, 
                MathHelper.PiOver4 / 120, 
                MathHelper.PiOver4 / 120);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            for (int i = 0; i < 6; i++)
            {
                GraphicsDevice.SetVertexBuffer(vertexBuffer[i]);

                // Set object and camera info
                effect[i].World = worldRotation * worldTranslation; // this would scale the Matrix: * Matrix.CreateScale(0.25f);
                effect[i].View = camera.View;
                effect[i].Projection = camera.Projection;
                //effect.VertexColorEnabled = true; // For VertexPositionColor
                effect[i].Texture = texture[i];
                effect[i].TextureEnabled = true;

                // Begin effect and draw for each pass
                foreach (EffectPass pass in effect[i].CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip,
                        vertices[i], 0, 2);
                }
            }

            base.Draw(gameTime);
        }
    }
}
