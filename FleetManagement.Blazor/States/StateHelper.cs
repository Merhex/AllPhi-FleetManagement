using Blazorise.DataGrid;
using System.Collections.Generic;

namespace FleetManagement.Blazor.States
{
    public static class StateHelper
    {
        public static IEnumerable<ColumnState> GetColumnState(List<DataGridColumnInfo> columns)
        {
            foreach (var column in columns)
            {
                yield return new ColumnState
                {
                    Direction = column.Direction,
                    Field = column.Field
                };
            }
        }
    }
}
