using System.Collections;
using System.Collections.Generic;
using System;

public class Pool<T>
{
    private readonly List<T> m_totalItems = new List<T>();
    public readonly Queue<T> m_deallocatedItems = new Queue<T>();
 
    private readonly Func<T> m_allocFunc;
	private readonly Action<T> m_deallocAction;
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="allocFunc"> code to be performed when new item is instanced </param>
    /// <param name="deallocAction"> code to be performed on deallocated item</param>
	public Pool(Func<T> allocFunc, Action<T> deallocAction = null)
    {
        m_allocFunc = allocFunc;
		m_deallocAction = deallocAction;
    }
	
    /// <summary>
    /// Move item to deallocated pool.
    /// </summary>
    public void Deallocate(T item)
    {
		if (!m_deallocatedItems.Contains(item))
        	m_deallocatedItems.Enqueue(item);
		if (m_deallocAction != null)
			m_deallocAction(item);
    }
 
    /// <summary>
    /// Instance new item, or use a pooled item
    /// </summary>
    /// <returns></returns>
    public T Allocate()
    {
        if (m_deallocatedItems.Count == 0)
        {
            T item = m_allocFunc();
            m_totalItems.Add(item);
 
            return item;
        }
 
        return m_deallocatedItems.Dequeue();
    }	
}