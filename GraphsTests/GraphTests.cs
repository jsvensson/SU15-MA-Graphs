using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Tests
{
	[TestClass()]
	public class GraphTests
	{
		[TestMethod()]
		public void GetNeighbours__4_Nodes__2_Neighbours()
		{
			Graph graph = new Graph(4);

			graph.AddEdges(
				new uint[6,2] {{1,1}, {3,0}, {2,3}, {2,1}, {0,1}, {2,0}}
			);

			List<uint> neighbours = graph.GetNeighbours(3);

			Assert.IsTrue(
				neighbours.Count == 2 &&
				neighbours.Contains(0) &&
				neighbours.Contains(2)
			);
		}

		[TestMethod()]
		public void GetNeighbours__1024_Nodes__No_Neighbours()
		{
			Graph graph = new Graph(1024);

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

			graph.AddEdges(
				new uint[1, 2] { { 103, 103 } }
			);

			List<uint> neighbours = graph.GetNeighbours(103);

			Assert.IsTrue(neighbours.Count == 1 && neighbours[0] == 103);
		}

		// Testa DoDFS() isConnected

		[TestMethod]
		public void DoDFS__4_Nodes__3_Edges__Is_Connected()
		{
			Graph graph = new Graph(4);

			graph.AddEdges(
				new uint[3, 2] { {0, 1}, {1, 2}, {2, 3} }
			);

			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsTrue(result.isConnected);
		}

		[TestMethod]
		public void DoDFS__4_Nodes__2_Edges__Is_Not_Connected()
		{
			Graph graph = new Graph(4);

			graph.AddEdges(
				new uint[2, 2] { {0, 1}, {2, 3} }
			);
			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsFalse(result.isConnected);
		}

		[TestMethod]
		public void DoDFS__4_Nodes__0_Edges__Is_Not_Connected()
		{
			Graph graph = new Graph(4);
			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsFalse(result.isConnected);
		}

		// Testa DoDFS() hasLoop

		[TestMethod]
		public void DoDFS__4_Nodes__3_Edges__Has_No_Loop()
		{
			Graph graph = new Graph(4);
			graph.AddEdges(
				new uint[3, 2] { {0,1}, {1,2}, {2,3} }
			);

			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsFalse(result.hasLoop);
		}

		[TestMethod]
		public void DoDFS__3_Nodes__3_Edges__Has_Loop()
		{
			Graph graph = new Graph(3);
			graph.AddEdges(
				new uint[3, 2] { {0, 1}, {1, 2}, {2, 0} }
			);

			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsTrue(result.hasLoop);
		}

		[TestMethod]
		public void DoDFS__8_Nodes__5_Edges__Has_Loop__Has_Orphans()
		{
			Graph graph = new Graph(8);
			graph.AddEdges(
				new uint[8, 2] { {0, 1}, {1, 2}, {2, 3}, {3, 0}, {0, 4}, {1, 5}, {2, 6}, {3, 7} }
			);

			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsTrue(result.hasLoop);
		}

		[TestMethod]
		public void DoDFS__9_nodes__8_Edges__Has_No_Loop__Has_Orphans()
		{
			Graph graph = new Graph(9);
			graph.AddEdges(
				new uint[8,2]
				{
					{0, 1}, {1, 2}, {2, 3}, {2, 8}, {3, 4}, {4, 5}, {4, 6}, {4, 7}
				}
			);
			Graph.DoDFSReturn result = graph.DoDFS();
			Assert.IsFalse(result.hasLoop);
		}
	}
}
