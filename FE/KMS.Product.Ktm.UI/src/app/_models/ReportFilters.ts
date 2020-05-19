//TODO: convert any to explicit type, if possible.
export class ReportFilters {
  selectedKudosType?: any;
  selectedTeam?: any;
  dateRange?: Date[];
  subFilter: {
    detailReportType: string; //todo: update string to enum
    visible: boolean;
    data: any;
  };
}
