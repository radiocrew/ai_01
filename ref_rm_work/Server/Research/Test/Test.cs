using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Collections.Generic;
//using MS.Internal;
//using Priority_Queue;

using RM.Server.Common;

namespace RND
{
    public class Test
    {
        public const string TEST_ARENA_KEY = "37b5dba2-3067-425a-80e8-0f06750c97c8";

        public void First()
        {
            Fourth();
            Third();
            Second();
            Dictionary<RUID, int> dic = new Dictionary<RUID, int>();

            RUID firstkey = RUID.ToRUID(TEST_ARENA_KEY);
            RUID ruid = RUID.ToRUID(TEST_ARENA_KEY);

            RUID ArenaKey = ruid;

            dic[firstkey] = 1;

            if (ArenaKey == firstkey)
            {
                Console.WriteLine("t");
            }
            else
            {
                Console.WriteLine("f");
            }

            dic[ArenaKey] = 3;// need return m_guid.GetHashCode();

            return;
        }

        public void Second()
        {
            RUID r = new RUID();
            r.IsEmpty();

            Guid g1 = Guid.NewGuid();
            Guid g2 = g1;

            //RUID r1 = new RUID(g1);
            //RUID r2 = new RUID(g2);
            //r1.Generate();

            RUID r1 = new RUID(g1);
            RUID r2 = r1;
            r2.Generate();

            if (r1 == r2)
            {
                Console.WriteLine("t");
            }
            else
            {
                Console.WriteLine("f");
            }

            Dictionary<RUID, int> m = new Dictionary<RUID, int>();
            m[r1] = 1;

            var old_hash = m[r1].GetHashCode();
            


            r1.Generate();// <<★

            var new_hash = m[r1].GetHashCode();

            if (r1 == r2)
            {
                Console.WriteLine("t");
            }
            

            m[r2] = 2;
        }

        public void Third()
        {
            RUID arenakey = RUID.ToRUID(TEST_ARENA_KEY);
            Dictionary<RUID, int> dic = new Dictionary<RUID, int>();
            dic[arenakey] = 2;


            RUID memberarenakey = arenakey;
            RUID newid = RUID.NewRuid();
            memberarenakey = newid;

            int val = 0;
            dic.TryGetValue(memberarenakey, out val);



        }

        public void Fourth()
        {
            List<MatchingInfo> matchingList = new List<MatchingInfo>();
            matchingList.Add(new MatchingInfo(10, 1));
            matchingList.Add(new MatchingInfo(20, 2));
            matchingList.Add(new MatchingInfo(10, 3));
            matchingList.Add(new MatchingInfo(20, 4));
            matchingList.Add(new MatchingInfo(10, 5));
            matchingList.Add(new MatchingInfo(20, 6));
            matchingList.Add(new MatchingInfo(30, 7));
            matchingList.Add(new MatchingInfo(30, 8));
            matchingList.Add(new MatchingInfo(30, 9));
            




            //-matching 하기,
            int PLAYER_COUNT = 2;
            int ARENA_ID = 30;

            
            bool made = false;

            List<MatchingInfo> matchedList = new List<MatchingInfo>();
            



            matchingList.Where(waiter => (ARENA_ID == waiter.ArenaID)).All(
                find => {
                    if (PLAYER_COUNT > matchedList.Count)
                    {
                        matchedList.Add(find);
                    }

                    return true;
                });

           
            matchingList.RemoveAll(matching => matchedList.Contains(matching));

            
            



            return;


            
        //-timeout 처리 -할까말까.


        //-cancel 하기.
            if (0 != matchingList.Count)
            {
                var first = matchingList.First();
                var last = matchingList.Last();

                int id = 3;
                matchingList.RemoveAll(info => (info.PlayerUID == id));
            }










        }

        public void Do()
        {
            {
                Guid g1 = Guid.NewGuid();
                Guid g2 = g1;

                //RUID r1 = new RUID(g1);
                //RUID r2 = new RUID(g2);
                //r1.Generate();

                RUID r1 = new RUID(g1);
                RUID r2 = r1;
                //r2.Generate();

                if (r1 == r2)
                {
                    Console.WriteLine("t");
                }
                else
                {
                    Console.WriteLine("f");
                }

                Dictionary<RUID, int> m = new Dictionary<RUID, int>();
                m[r1] = 1;
                r1.Generate();
                m[r2] = 2;


                


                RUID n1 = new RUID(g1);

                //n1 = r2;
                //n1.Reset(Guid.NewGuid());

                int val = 0;
                if (false == m.TryGetValue(n1, out val))
                {
                    return;
                }

                return;







            }

        }
    }
}
