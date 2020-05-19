import { SelectFilter } from './SelectFilter';

export const ListOfDummyTeams: SelectFilter[] = [{name: "Default", value: "1", disabled: false}, {name: "Default 2", value: "2", disabled: false}]
export const ListOfDummyTypes: SelectFilter[] = [
  {name: "Kudos", value: "1"},
  {name: "Gift", value: "2", disabled: false},
  {name: "Compliment", value: "3", disabled: true},
  {name: "Travel abroad", value: "4", disabled: true}
];