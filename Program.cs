//By: Akhier Dragonheart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libtcod;
using CSharpHelperCode;
using CSharpSimpleMapGen;
using CSharpDijkstraAlgorithm; //the tiles are 8x8
namespace CSharpShowDijkstraWorking {
    class Program {
        static void Main(string[] args) {
            TCODConsole.initRoot(80, 50, "Showing Dijkstra Algorithm Working");
            TCODSystem.setFps(30);
            Graph map = makeMap();
            drawMap(map.AllNodes);
            TCODMouseData mData;
            while (!TCODConsole.isWindowClosed()) {
                TCODConsole.root.print(1,1, TCODSystem.getFps().ToString());
                TCODConsole.flush();
                mData = TCODMouse.getStatus();
                if (mData.LeftButtonPressed) {
                    
                }
            }
        }

        static Graph makeMap() {
            Graph output = new Graph();
            bool[,] boolMap = MapGen.newMap(80, 50, true);
            Vector2D[,] vectorMap = new Vector2D[80, 50];
            for (int column = 0; column < 50; column++) {
                for (int row = 0; row < 80; row++) {
                    if (boolMap[row, column]) {
                        vectorMap[row, column] = new Vector2D(row, column, false);
                        output.addVector(vectorMap[row, column]);
                    }
                }
            }
            for (int column = 1; column < 49; column++) {
                for (int row = 1; row < 79; row++) {
                    if (boolMap[row, column]) {
                        if (boolMap[row - 1, column - 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row - 1, column - 1], 11)); }
                        if (boolMap[row - 1, column]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row - 1, column], 10)); }
                        if (boolMap[row - 1, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row - 1, column + 1], 11)); }
                        if (boolMap[row, column - 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row, column - 1], 10)); }
                        if (boolMap[row, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row, column + 1], 10)); }
                        if (boolMap[row + 1, column - 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row + 1, column - 1], 11)); }
                        if (boolMap[row + 1, column]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row + 1, column], 10)); }
                        if (boolMap[row + 1, column + 1]) { output.addEdge(new Edge(vectorMap[row, column], vectorMap[row + 1, column + 1], 11)); }
                    }
                }
            }
            return output;
        }
        static void drawMap(List<Vector2D> floormap) {
            TCODConsole.root.setBackgroundColor(new TCODColor(15, 15, 15));
            TCODConsole.root.setForegroundColor(TCODColor.lightestGrey);
            TCODConsole.root.clear();
            for (int column = 0; column < 50; column++) {
                for (int row = 0; row < 80; row++) {
                    TCODConsole.root.putChar(row, column, '#');
                }
            }
            foreach (Vector2D tile in floormap) {
                TCODConsole.root.putChar(tile.X, tile.Y, '.');
            }
            TCODConsole.flush();
        }
    }
}
