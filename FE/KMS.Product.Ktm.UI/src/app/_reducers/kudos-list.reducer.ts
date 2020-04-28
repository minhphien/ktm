import { createReducer, props, createAction, on, Action } from "@ngrx/store";
import { Kudos, KudosState } from '@app/_models';

function initialAppState(){
	return <KudosState>{ 
	}
}

export const updateKudos = createAction('[kudos] update', props<KudosState>());

const _createReducer = createReducer(
	initialAppState(), 
	on(updateKudos, (state: KudosState, newState: KudosState) => <KudosState> newState)
);

export function kudosStateReducer(state: KudosState, action: Action) {
	return _createReducer(state,action);
}

export const selectKudosList = (state: KudosState) => state;
export const selectKudosReceived = (state: KudosState) => state.kudoReceives;
export const selectKudosSent = (state: KudosState) => state.kudoSends;

