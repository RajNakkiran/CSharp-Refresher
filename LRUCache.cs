using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/*
 * Raj Nakkiran. Aug 13,2019
 * LRU Cache needs the following API
 * Save upto MAX_COUNT blocks of data with a given starting address
 * Add a block, given start addr  
 * Get if a block exists given a start addr; update timestamp if exits. 
 * Remove oldest block to make make way  for a new block
 * Find number of blocks in LRU
 */

namespace LRUCache
{

    class MemBlock
    {
        public DateTime lastUsedTime;   // bookkeeping extra in front of every real memory block
        public int someValue;           // real memory block
        public MemBlock()
        {
            lastUsedTime = DateTime.Now;
            someValue = 99;
        }
    }

    class LRUCache
    {
        const int MAX_COUNT = 10;
        static Dictionary<int, MemBlock> dict = new Dictionary<int, MemBlock>();   // addr, blockValue
        static SortedDictionary<DateTime, int> lru = new SortedDictionary<DateTime, int>();  // timestamp, addr 

        static int GetCount()
        {
            return dict.Count;
        }

        static void AddBlock(int addr, MemBlock b)
        {
            if ( GetCount() >  MAX_COUNT )
            {
                KeyValuePair<DateTime, int> kp = lru.First();
                lru.Remove(kp.Key);
                dict.Remove(kp.Value);
                Console.WriteLine("REMOVE: time = {0}  addr = {1}", kp.Key.ToString("ffffff"), kp.Value);
            } 

            b.lastUsedTime = DateTime.Now;
            dict[addr] = b;
            lru.Add(b.lastUsedTime, addr);
            Console.WriteLine("ADD: time = {0}  addr = {1}", b.lastUsedTime.ToString("ffffff"), addr);

        }

        static MemBlock FindBlock( int addr )
        {
            if ( dict.ContainsKey( addr ))
            {
                MemBlock b = dict[addr];
                lru.Remove(b.lastUsedTime);
                b.lastUsedTime = DateTime.Now;
                dict[addr] = b;  // updated 
                lru.Add(b.lastUsedTime, addr);
                Console.WriteLine("FOUND: {0}", addr);
                return b;
            }
            Console.WriteLine("NOT FOUND: {0}", addr);
            return null;
        }

        public static MemBlock ReadBlock(int addr)
        {
            MemBlock b = FindBlock(addr);
            if ( b == null)
            {
                // Simulate reading from main memory 
                b = new MemBlock();
                AddBlock(addr, b);
            }
            return b;

        }

        public static void WriteBlock(int addr, MemBlock b)
        {
            MemBlock b2 = FindBlock(addr);
            if (b2 == null)
            {
                AddBlock(addr, b);
            } else
            {
                b2.someValue = b.someValue;
            }
        }


        static int GetExistingAddressForTest()
        {
            Random rn = new Random();
            int pick = rn.Next(0, GetCount());
            int i = 0;
            foreach( KeyValuePair<int, MemBlock> kp in dict)
            {
                if ( i == pick)
                {
                    Console.WriteLine("REUSER: addr = {0}", kp.Key);
                    return kp.Key;
                }
                i++;
            }
            return -1;
        }

        static void Main(string[] args)
        {
            Random rn = new Random();
            for(int i = 0; i < 25; i++)
            {
                Thread.Sleep(3);                   // make time stamps distint 

                int addr = -1;
                if ( (rn.Next() % 2) == 0 )
                {
                    addr = GetExistingAddressForTest();
                }
                
                if ( addr == -1 )
                {
                    addr = rn.Next(1000, 9999);
                }

                int op = (rn.Next() % 2);           // decide read or write 
                MemBlock b;

                if ( op == 0 )
                {
                    b = ReadBlock(addr);
                }
                else
                {
                    b = new MemBlock();
                    WriteBlock(addr, b);
                }
            }

            // print final status
            Console.WriteLine("FINAL CACHE");
            foreach (KeyValuePair<DateTime,int> kp in lru ) {
                Console.WriteLine("{0}:{1}", kp.Key.ToString("ffffff"), kp.Value );
            }

        }
    }
}
