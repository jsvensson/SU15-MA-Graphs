using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
	public class Graph
	{
		private bool[,] adjMatr;

		public struct DoDFSReturn
		{
			public bool hasLoop;
			public bool isConnected;
		}

		private uint nrNodes;

		public uint NrNodes
		{
			get
			{
				return this.nrNodes;
			}
			private set
			{
				if( value == 0 )
				{
					throw new ArgumentOutOfRangeException(
						"The graph must have atleast one node.",
						"NrNodes"
					);
				}

				this.nrNodes = value;
			}
		}

		public Graph( uint nrNodes )
		{
			this.NrNodes = nrNodes;
			this.adjMatr = new bool[nrNodes,nrNodes];
		}

		public List<uint> GetNeighbours( uint node )
		{
			if (node >= this.NrNodes)
			{
				throw new ArgumentOutOfRangeException(
					$"This graph has {this.NrNodes} nodes and thus " +
					$"no node named '{node}'"
				);
			}

			List<uint> neighbours = new List<uint>();

			for( uint i = 0; i < this.adjMatr.GetLength(1); i++ )
			{
				if( this.adjMatr[node,i] == true )
				{
					neighbours.Add(i);
				}
			}

			return neighbours;
		}

		public void AddEdges( uint[,] edges )
		{
			if( edges.GetLength(1) != 2 )
			{
				throw new ArgumentException(
					"The second dimension of arg edges must be 2.",
					"edges"
				);
			}

			foreach( uint node in edges )
			{
				if( node >= this.NrNodes )
				{
					throw new ArgumentOutOfRangeException(
						$"This graph has {this.NrNodes} nodes and thus "+
						$"no node named '{node}'",
						"edges"
					);
				}
			}

			for( uint i = 0; i < edges.GetLength( 0 ); i++ )
			{
				this.adjMatr[edges[i, 0], edges[i, 1]] = true;
				this.adjMatr[edges[i, 1], edges[i, 0]] = true;
			}
		}

		public DoDFSReturn DoDFS( uint startNode = 0 )
		{
			if (startNode>= this.NrNodes)
			{
				throw new ArgumentOutOfRangeException(
					$"This graph has {this.NrNodes} nodes and thus " +
					$"no node named '{startNode}'",
					"startNode"
				);
			}

			Stack<uint> stack = new Stack<uint>();
			bool[] visited = new bool[NrNodes];

			stack.Push(startNode);

			DoDFSReturn result = new DoDFSReturn
			{
				isConnected = true
			};

			while (stack.Count > 0)
			{
				uint currentNode = stack.Pop();
				int visitedNeighborCount = 0;

				if (!visited[currentNode])
				{
					List<uint> neighbors = GetNeighbours(currentNode);
					visited[currentNode] = true;
					foreach (uint neighbor in neighbors)
					{
						// Är grannen besökt?
						if (visited[neighbor])
						{
							visitedNeighborCount++;
						}
						stack.Push(neighbor);
					}
					// Besökt 2+ grannar?
					if (visitedNeighborCount >= 2)
					{
						result.hasLoop = true;
					}
				}
			}

			// Besökt alla noder?
			foreach (bool v in visited)
			{
				if (v == false)
				{
					result.isConnected = false;
					break;
				}
			}

			return result;
		}

		public static void Main()
		{

		}
	}
}
