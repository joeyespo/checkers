using System;
using System.Collections;

namespace Checkers
{
  public class TcpClientCollection : CollectionBase, IEnumerable
  {
    
    public int Add (TcpClient item)
    { return InnerList.Add(item); }
    
    public void AddRange (TcpClient [] items)
    { foreach (TcpClient item in items) Add(item); }
    
    public void Insert (int index, TcpClient item)
    { InnerList.Insert(index, item); }
    
    public void Remove (TcpClient item)
    { InnerList.Remove(item); }
    
    public bool Contains (TcpClient item)
    { return InnerList.Contains(item); }
    
    public int IndexOf (TcpClient item)
    { return InnerList.IndexOf(item); }
    
    public void CopyTo (TcpClient [] array, int index)
    { InnerList.CopyTo(array, index); }
    
    public TcpClient this [int index]
    {
      get { return (TcpClient)InnerList[index]; }
      set { InnerList[index] = value; }
    }
    
    public TcpClient [] ToArray ()
    { return (TcpClient [])InnerList.ToArray(typeof(TcpClient)); }
    
  }
}
