using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Battery : GameObject , IAttachable
    {
		// @Paul Private?
        int _powerlvl;
		// @Paul Spacing?
        public Battery(string name, float width, float height,int initalPowerlvl) : base(name, width, height)
        {
            _powerlvl = initalPowerlvl;
        }
        public int Power
        {
            get { return _powerlvl; }
        }
		// @Paul So does this add power becuase ChangePower Makes me think it sets it I might just be crazy
        public bool ChangePower(int powerchange)
        {
            if (_powerlvl + powerchange < 0)
            {      
                return false;
            }
            _powerlvl = _powerlvl + powerchange;
            return true;
        }

        public override void Render()
        {
            SwinGame.FillRectangle(Color.Blue,X,Y,Width,Height);
        }
		// @Paul What does this do?
        public override void Update()
        {
            
        }
    }
}
