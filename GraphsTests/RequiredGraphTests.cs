// *************************************
// *************************************

// WARNING!

// YOU ARE *NOT* ALLOWED TO MAKE ANY CHANGES TO THIS FILE AT ALL!
// IF YOU MAKE CHANGES TO THIS FILE THEN YOUR SUBMISSION WON'T BE ACCEPTED!

// THIS FILE CONTAINS UNIT TESTS THAT YOUR PROJECT MUST PASS. IF YOUR
// PROJECT OBTAINS COMPILATION ERRORS WITH THESE TESTS OR IF IT FAILS ANY OF
// THESE TESTS, THEN YOUR PROJECT CONTAINS ERRORS *OUTSIDE*OF*THIS* FILE
// NAMED "RequiredGraphTests.cs" AND YOU MUST FIX THEM.
// YOU ARE *NOT* ALLOWED TO MODIFY ANY FAILING TESTS IN THIS FILE.

// If you strongly feel that this file is incorrect in any way, i.e. one of
// its unit tests fails
// because the test itself contains an error, then contact me (Arash).
// But DON'T change anything unless I allow you to.

// *************************************
// *************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoDFSReturn = Graphs.Graph.DoDFSReturn;

namespace Graphs.Tests
{
	[TestClass()]
	public class RequiredGraphTests
	{
		private static Graph TestDoDFS(
			uint nrNodes, uint[,] edges, bool isConnected, bool hasLoop
		)
		{
			return TestDoDFS( nrNodes, edges, 0, isConnected, hasLoop );
		}

		private static Graph TestDoDFS(
			uint nrNodes, uint[,] edges, uint startNode, bool isConnected, bool hasLoop
		)
		{
			Graph graph = new Graph( nrNodes );

			graph.AddEdges( edges );

			DoDFSReturn dFSReturn = graph.DoDFS( startNode );

			Assert.IsTrue(
				dFSReturn.isConnected == isConnected,
				isConnected ?
					"Graph should be connected" :
					"Graph shouldn't be connected"
			);

			Assert.IsTrue(
				dFSReturn.hasLoop == hasLoop,
				hasLoop ?
					"Searched component should have a loop" :
					"Searched component shouldn't have a loop"
			);

			return graph;
		}

		[TestMethod()]
		public void GetNeighbours__4_Nodes__2_Neighbours()
		{
			Graph graph = new Graph( 4 );

			graph.AddEdges(
				new uint[6,2] {{1,1}, {3,0}, {1,3}, {2,1}, {0,1}, {2,0}}
			);

			List<uint> neighbours = graph.GetNeighbours( 3 );

			Assert.IsTrue(
				neighbours.Count == 2 &&
				neighbours.Contains( 0 ) &&
				neighbours.Contains( 1 )
			);
		}

		[TestMethod()]
		public void GetNeighbours__1024_Nodes__No_Neighbours()
		{
			Graph graph = new Graph( 1024 );

			graph.AddEdges(
				new uint[3, 2] { { 732, 254 }, { 958, 121 }, { 24, 654 } }
			);

			List<uint> neighbours = graph.GetNeighbours(65);

			Assert.IsTrue( neighbours.Count == 0 );
		}

		[TestMethod()]
		public void GetNeighbours__128_Nodes__Node_Is_Its_Own_Neighbour()
		{
			Graph graph = new Graph(128);

			graph.AddEdges( new uint[1, 2] { { 103, 103 } } );

			List<uint> neighbours = graph.GetNeighbours( 103 );

			Assert.IsTrue( neighbours.Count == 1 && neighbours[0] == 103 );
		}

		[TestMethod()]
		[ExpectedException( typeof( AssertFailedException ) )]
		public void TestDoDFS__1_Node__IsCon__Exp_Asrt_Failed()
		{
			TestDoDFS( 1, new uint[ 0, 2 ], false, false );
		}

		[TestMethod()]
		[ExpectedException( typeof( AssertFailedException ) )]
		public void TestDoDFS__1_Node__HasNoLoop__Exp_Asrt_Failed()
		{
			TestDoDFS( 1, new uint[ 0, 2 ], true, true );
		}

		[TestMethod()]
		public void DoDFS__1_Node__Method_Returns_Graph()
		{
			Graph graph = TestDoDFS( 1, new uint[ 0, 2 ], true, false );

			List<uint> neighbs = graph.GetNeighbours( 0 );
			DoDFSReturn dFSReturn = graph.DoDFS();

			Assert.IsTrue( graph.NrNodes == 1 );
			Assert.IsTrue( neighbs.Count == 0 );
			Assert.IsTrue(
				dFSReturn.hasLoop == false && dFSReturn.isConnected == true
			);
		}

		[TestMethod()]
		public void DoDFS__1_Node__No_Edges__Is_Con__Has_No_Loop()
		{
			TestDoDFS( 1, new uint[ 0, 2 ], true, false );
		}

		[TestMethod()]
		public void DoDFS__2_Nodes__No_Edges__Is_Not_Con__Has_No_Loop()
		{
			TestDoDFS( 2, new uint[ 0, 2 ], false, false );
		}

		[TestMethod()]
		public void DoDFS__2_Nodes__1_Edge__Is_Con__Has_No_Loop()
		{
			TestDoDFS( 2, new uint[,] { { 1, 0 } }, true, false );
		}

		[TestMethod()]
		public void DoDFS__3_Nodes__1_Edge__Is_Not_Con__Has_No_Loop()
		{
			TestDoDFS( 3, new uint[,] { { 1, 2 } }, false, false );
		}

		[TestMethod()]
		public void DoDFS__3_Nodes__2_Edges__Is_Con__Has_No_Loop()
		{
			TestDoDFS( 3, new uint[,] { { 1, 2 }, { 1, 0 } }, true, false );
		}

		[TestMethod()]
		public void DoDFS__3_Nodes__3_Edges__Is_Con__Has_Loop()
		{
			TestDoDFS(
				3, new uint[,] { { 1, 2 }, { 0, 2 }, { 1, 0 } }, true, true
			);
		}

		[TestMethod()]
		public void DoDFS__4_Nodes__3_Edges__Is_Not_Con__Has_Loop()
		{
			TestDoDFS(
				4, new uint[,] { { 1, 2 }, { 0, 2 }, { 1, 0 } }, false, true
			);
		}

		[TestMethod()]
		public void DoDFS__5_Nodes__4_Edges__Is_Not_Con__Has_Loop()
		{
			TestDoDFS(
				5, new uint[,] { { 1, 2 }, { 0, 2 }, { 1, 0 }, { 3, 4 } }, false, true
			);
		}

		[TestMethod()]
		public void DoDFS__4_Nodes__3_Edges__Start_Node_Alone__Is_Not_Con__Comp_Has_No_Loop()
		{
			TestDoDFS(
				4, new uint[,] { { 1, 2 }, { 0, 2 }, { 1, 0 } }, 3, false, false
			);
		}

		[TestMethod()]
		public void DoDFS__5_Nodes__4_Edges__Start_Node_With_Another_Node__Is_Not_Con__Comp_Has_No_Loop()
		{
			TestDoDFS(
				5, new uint[,] { { 1, 2 }, { 0, 2 }, { 1, 0 }, { 3, 4 } },
				3, false, false
			);
		}

		[TestMethod()]
		public void DoDFS__4_Nodes__4_Edges__One_Node_Not_In_Loop__Is_Con__Has_Loop()
		{
			TestDoDFS(
				4, new uint[,] { { 0, 1 }, { 1, 2 }, { 2, 0 }, { 1, 3 } }, true, true
			);
		}

		[TestMethod()]
		public void DoDFS__4_Nodes__4_Edges__Start_Node_Neighb_Not_In_Loop__Is_Con__Has_Loop()
		{
			TestDoDFS(
				4, new uint[,] { { 0, 1 }, { 1, 2 }, { 2, 0 }, { 0, 3 } }, true, true
			);
		}

		[TestMethod()]
		public void DoDFS__9_Nodes__8_Edges__Star_Shaped__Is_Con__Has_No_Loop()
		{
			TestDoDFS(
				9,
				new uint[,]
				{
					{ 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 },
					{ 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 }
				},
				true,
				false
			);
		}

		[TestMethod()]
		public void DoDFS__9_Nodes__8_Edges__Star_Shaped__With_A_Triangle__Is_Con__Has_Loop()
		{
			TestDoDFS(
				9,
				new uint[,]
				{
					{ 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 },
					{ 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 }, { 3, 4 }
				},
				true,
				true
			);
		}

		[TestMethod()]
		public void DoDFS__9_Nodes__8_Edges__Star_Shaped__Start_Node_At_Corner__Is_Con__Has_No_Loop()
		{
			TestDoDFS(
				9,
				new uint[,]
				{
					{ 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 },
					{ 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 }
				},
				5,
				true,
				false
			);
		}

		[TestMethod()]
		public void DoDFS__17_Nodes__16_Edges__Star_Shaped__Two_Edges_Per_Branch__Is_Con__Has_No_Loop()
		{
			TestDoDFS(
				17,
				new uint[,]
				{
					{ 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 },
					{ 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
					{ 1, 9 }, { 2, 10 }, { 3, 11 }, { 4, 12 },
					{ 5, 13 }, { 6, 14 }, { 7, 15 }, { 8, 16 }
				},
				true,
				false
			);
		}

		[TestMethod()]
		public void DoDFS__17_Nodes__16_Edges__Star_Shaped__Two_Edges_Per_Branch__Has_Triangle__Is_Con__Has_Loop()
		{
			TestDoDFS(
				19,
				new uint[,]
				{
					{ 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 },
					{ 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
					{ 1, 9 }, { 2, 10 }, { 3, 11 }, { 4, 12 },
					{ 5, 13 }, { 6, 14 }, { 7, 15 }, { 8, 16 },
					{ 16, 17 }, { 17, 18 }, { 18, 16 }
				},
				true,
				true
			);
		}

		[TestMethod()]
		public void DoDFS__9_Nodes__9_Edges__4_Node_Loop_In_Middle_With_Outgoing_Branches__Is_Con__Has_Loop()
		{
			TestDoDFS(
				9,
				new uint[,]
				{
					{ 0, 3 }, { 0, 2 }, { 0, 1 }, { 2, 5 },
					{ 2, 4 }, { 5, 6 }, { 4, 7 }, { 4, 1 },
					{ 1, 8 }
				},
				true,
				true
			);
		}
	}
}
