﻿using FleetManagement.API.Read.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FleetManagement.API.Read.Queries
{
    public class MotorVehiclesQuery : IRequest<IPaginatedResponse<MotorVehicleResponse>>, IPaginatedQuery, ISortableQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }

        public string ChassisNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public bool? Operational { get; set; }
    }
}
