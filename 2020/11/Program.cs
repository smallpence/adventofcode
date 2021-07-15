using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace _11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string[] lines = File.ReadAllLines("input");

            SeatGrid seatGrid = new SeatGrid(lines[0].Length, lines.Length);

            lines.ToList().ForEach(seatGrid.LoadLine);

            seatGrid.Print();

            while (seatGrid.Step()) {
                seatGrid.Print();
                // Console.ReadKey(true);
            }

            Console.WriteLine(seatGrid.CountOccupied());
        }
    }

    class SeatGrid
    {
        private char[][] seats;
        private int width;
        private int height;

        public SeatGrid(int width, int height)
        {
            seats = new char[height][];

            loadLine = 0;
            this.width = width;
            this.height = height;
        }

        private int loadLine;
        public void BeginLoad() {
            loadLine = 0;
        }

        public void LoadLine(string line) {
            seats[loadLine++] = line.ToCharArray();
        }

        public int GetAdjacentVisibleTakenSeats(int x, int y) {
            int count = 0;

            // for (int dx = x - 1; dx <= x + 1; dx++) for (int dy = y - 1; dy <= y + 1; dy++)
            for (int dx = -1; dx <= 1; dx++) for (int dy = -1; dy <= 1; dy++)
                {
                    // if (!(x == dx && y == dy) && IsTaken(dx,dy)) count++;                    

                    if (dx == 0 && dy == 0) continue;
                    int cx = x, cy = y;
                    SeatResult result;
                    do
                    {
                        cx += dx;
                        cy += dy;
                        result = CheckState(cx,cy);
                    } while (!(result == SeatResult.OCCUPIED || result == SeatResult.OUT_OF_BOUNDS || result == SeatResult.FREE));

                    if (result == SeatResult.OCCUPIED) count++;
                }

            return count;
        }

        public SeatResult CheckState(int x, int y) {
            if (x < 0 || y < 0 || x >= width || y >= height) return SeatResult.OUT_OF_BOUNDS;

            switch (seats[y][x])
            {
                case 'L': return SeatResult.FREE;
                case '#': return SeatResult.OCCUPIED;
                case '.': return SeatResult.NOT_CHAIR;
            }

            throw new Exception("Unknown value of seats[y][x]");
        }

        public void ForEachSeat(Action<char,int,int> handler) {
            int x = 0, y = 0;
            foreach (char[] row in seats)
            {
                foreach (char seat in row)
                {
                    if (seat != '.') handler.Invoke(seat,x,y);
                    x++;
                }
                x = 0;
                y++;
            }
        }

        public void SetSeat(int x, int y, char seat) {
            // Console.WriteLine("set {0},{1} to {2}",x,y,seat);
            // Print();
            seats[y][x] = seat;
        }

        public void SetSeat(Action action) {
            SetSeat(action.x,action.y,action.newChar);
        }

        public bool Step() {
            List<Action> actions = new List<Action>();

            ForEachSeat((seat,x,y) => {
                int adjacentTakenSeats = GetAdjacentVisibleTakenSeats(x,y);

                if (seat == 'L' && adjacentTakenSeats == 0) {
                    actions.Add(new Action(x,y,'#'));
                }

                if (seat == '#' && adjacentTakenSeats >= 5) {
                    actions.Add(new Action(x,y,'L'));
                }

                // Console.WriteLine("x:{0} y:{1}",x,y);
            });
            
            foreach (Action action in actions) SetSeat(action);

            return actions.Count > 0;
        }

        public void Print()
        {
            Console.WriteLine("begin grid");
            seats.ToList().ForEach(row => {
                row.ToList().ForEach(seat => Console.Write("{0} ",seat));
                Console.Write("\n");
            });
        }

        public int CountOccupied() {
            int count = 0;

            ForEachSeat((seat,x,y) => {
                if (seat == '#') count++;
            });

            return count;
        }
    }

    struct Action
    {
        public int x;
        public int y;
        public char newChar;

        public Action(int x,int y,char newChar) => (this.x,this.y,this.newChar) = (x,y,newChar);
    }

    enum SeatResult
    {
        FREE,
        OCCUPIED,
        NOT_CHAIR,
        OUT_OF_BOUNDS
    }
}