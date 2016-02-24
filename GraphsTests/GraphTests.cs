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
				neighbours.Contains( 0 ) &&
				neighbours.Contains( 2 )
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

			List<uint> neighbours = graph.GetNeighbours( 103 );

			Assert.IsTrue( neighbours.Count == 1 && neighbours[0] == 103 );
		}

		// Testa DoDFS()

		[TestMethod]
		public void DoDFS__4_Nodes__3_Edges__Is_Connected()
		{
			Graph graph = new Graph(4);

			graph.AddEdges(
				new uint[3, 2] { {0,1}, {1,2}, {2,3} }
			);

			Assert.IsTrue(graph.DoDFS());
		}

		[TestMethod]
		public void DoDFS__4_Nodes__3_Edges__Is_Not_Connected()
		{
			Graph graph = new Graph(4);

			graph.AddEdges(
				new uint[3, 2] { { 0, 1 }, { 1, 1 }, { 2, 3 } }
			);

			Assert.IsFalse(graph.DoDFS());
		}

		[TestMethod]
		public void DoDFS__4_Nodes__0_Edges__Is_Not_Connected()
		{
			Graph graph = new Graph(4);
			Assert.IsFalse(graph.DoDFS());
		}

	}
}
