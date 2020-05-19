import { SelectFilter } from './SelectFilter';

export const ListOfDummyTeams: SelectFilter[] = [
  {name: "Advengers", value: "1", disabled: false}, 
  {name: "Clearwave", value: "2", disabled: false},
  {name: "Pointivo", value: "3", disabled: false}
]
export const ListOfDummyTypes: SelectFilter[] = [
  {name: "Kudos", value: "1"},
  {name: "Gift", value: "2", disabled: false},
  {name: "Compliment", value: "3", disabled: true},
  {name: "Travel abroad", value: "4", disabled: true}
];

export const ListOfReports: SelectFilter[] = [
  { name: "Sent/Received kudos by one Team", routeUrl: "/report/kudos-by-team",  value: "1", disabled: false },
  { name: "Sent/Received kudos across Teams", routeUrl: "/report/kudos-across-team", value: "2", disabled: false },
  { name: "Sent/Received kudos by users", routeUrl: "/report/kudos-by-user", value: "3", disabled: false }
];