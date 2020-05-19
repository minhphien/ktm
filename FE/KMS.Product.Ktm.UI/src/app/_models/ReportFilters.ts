//TODO: convert any to explicit type, if possible.
export class ReportFilters {
  selectedReport: any;
  selectedKudosType?: any;
  selectedTeam?: any;
  selectedTeams?: any[];
  dateRange?: Date[];
  subFilter: {
    detailReportType: string; //todo: update string to enum
    visible: boolean;
    data: any;
  };
}
