//By: Akhier Dragonheart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libtcod;
using CSharpHelperCode;
using CSharpSimpleMapGen;
using CSharpDijkstraAlgorithm;
namespace CSharpShowDijkstraWorking {
    class Program {
        static void Main(string[] args) {
            TCODConsole.initRoot(80, 50, "Showing Dijkstra Algorithm Working");
            Graph map = new Graph();
            TCODConsole.waitForKeypress(true);            
        }

        static Graph makeMap() {
            Graph output = new Graph();
            bool[,] boolMap = MapGen.newMap(80, 50, true);
            for (int column = 0; column < 50; column++) {
                for (int row = 0; row < 80; row++) {
                    
                }
            }
            return output;
        }
    }
}
