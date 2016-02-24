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
			bool hasLoop;
			bool isConnected;
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

		public bool DoDFS( uint startNode = 0 )
		{
			if (startNode>= this.NrNodes)
			{
				throw new ArgumentOutOfRangeException(
					$"This graph has {this.NrNodes} nodes and thus " +
					$"no node named '{startNode}'",
					"startNode"
				);
			}

			return true;
		}

		public static void Main()
		{

		}
	}
}
