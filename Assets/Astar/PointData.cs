public class PointData {
    public APoint point;
    public float g;
    public float h;
    public PointData parent;

    public PointData(APoint p, float g, float h, PointData parent)
    {
        this.point = p;
        this.g = g;
        this.h = h;
        this.parent = parent;
    }

    public float f()
    {
        return this.g + this.h;
    }

}
