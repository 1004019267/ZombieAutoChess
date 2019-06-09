using System.Collections;
using System.Collections.Generic;

public class MinHeap
{

    private List<PointData> queue = new List<PointData>();
    private int endPnt = 0;
    private Dictionary<string, PointData> index = new Dictionary<string, PointData>();

    private string getKey(APoint pnt)
    {
        // return String.format("%d-%d", pnt.x, pnt.y);
        if (pnt == null)
        {
            return null;
        }
        return pnt.x + "-" + pnt.y;
    }

    public bool isEmpty()
    {
        return this.endPnt <= 0;
    }

    public PointData getAndRemoveMin()
    {
        if (this.isEmpty())
        {
            return null;
        }

        PointData head = queue[0];
        PointData last = queue[(this.endPnt - 1)];
        this.queue[0] = last;
        this.endPnt--;
        this.index.Remove(this.getKey(head.point));
        this.topDown();
        return head;
    }

    public PointData find(APoint pnt)
    {
        string key = this.getKey(pnt);
        if (index.ContainsKey(key))
        {

        return this.index[key];
        }
        return null;
    }

    public void add(PointData data)
    {
        if (this.queue.Count > this.endPnt)
        {
            this.queue[this.endPnt] = data;
        }
        else
        {
            this.queue.Add(data);
        }
        this.endPnt++;
        this.index.Add(this.getKey(data.point), data);
        this.bottomUp();
    }

    private void topDown()
    {
        for (int cur = 0; cur < this.endPnt;)
        {
            int left = 2 * cur + 1;
            int right = 2 * cur + 2;

            PointData dc = queue[cur];
            PointData dl = left < this.endPnt ? (PointData)this.queue[left] : null;
            PointData dr = right < this.endPnt ? (PointData)this.queue[right] : null;

            int next = -1;
            PointData dn = dc;
            if (dl != null && dl.f() < dn.f())
            {
                next = left;
                dn = dl;
            }
            if (dr != null && dr.f() < dn.f())
            {
                next = right;
                dn = dr;
            }

            if (next >= 0 && next < this.endPnt)
            {
                this.queue[next] = dc;
                this.queue[cur] = dn;
                cur = next;
            }
            else
            {
                break;
            }
        }

    }

    private void bottomUp()
    {
        for (int cur = this.endPnt - 1; cur >= 0;)
        {
            int parent = (cur - 1) / 2;
            if (parent < 0)
            {
                break;
            }

            PointData dc = (PointData)this.queue[cur];
            PointData dp = (PointData)this.queue[parent];

            if (dc.f() < dp.f())
            {
                this.queue[parent] = dc;
                this.queue[cur] = dp;
                cur = parent;
            }
            else
            {
                break;
            }
        }
    }
}
