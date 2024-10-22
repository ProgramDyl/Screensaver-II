using System.Drawing;
using Screensaver.Views;

namespace Screensaver.Models;

public abstract class Shape
{ 
    //region Basic Properties
    protected int X, Y, Width, Height;
    protected Color Color;
    
    protected int[] Velocity = new int[2];
    
    // Add properties for bounding box
    protected int Bx, By, BWidth, BHeight;
    
    //endregion
    
    //region Constructor
    protected Shape(int x, int y, int width, int height, Color color)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        this.Color = color;
        
        //speed
        InitializeVelocity();

    } 
   //endregion Constructor 
   
   public abstract void Draw(Graphics g, DrawPanel drawPanel);

   public void Move()
   {
       X += Velocity[0];
       Y += Velocity[1];
       
       // Declare bounding box size variables
       Bx = X;
       By = Y;
       BWidth = Width;
       BHeight = Height;
   }

   public bool CollidesWith(Shape other)
   {
       //Checks for collision between this shape's bounding box and the other shape's bounding box
       return this.Bx < other.Bx + other.BWidth &&
              this.Bx + this.BWidth > other.Bx &&
              this.By + this.BHeight > other.By;
   }

   public void ShapeCollision(Shape other)
   {
       //Check for collision with the other shapes
       if (this != other && this.CollidesWith(other))
       {
           //Detects a collision and updates the movement of the shape
           this.Velocity[0] = -this.Velocity[0];
           this.Velocity[1] = -this.Velocity[1];
           other.Velocity[0] = this.Velocity[0];
           other.Velocity[1] = this.Velocity[1];
       }
   }
    public void InitializeVelocity()
    {
        Random random = new Random();
        Velocity[0] = random.Next(-3, 3);
        Velocity[1] = random.Next(-3, 3);
    }

    protected void DetectEdge(int panelWidth, int panelHeight)
    {
        if (X <= 0 || X + Width >= panelWidth) Velocity[0] += -1;
        if (Y <= 0 || Y + Height >= panelHeight) Velocity[1] += -1;
    }
}