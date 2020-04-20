import { Action, createAction, props, createReducer,  on } from '@ngrx/store';
import { AppState, User } from '@app/_models';

// Actions
export const loading = createAction('[loader] LOADING');
export const loaded = createAction('[loader] LOADED');
export const updateUser = createAction('[userInfo] update', props<User>());
export const deleteUser = createAction('[userInfo] delete');

export const SESSION_USER_INFO: string = 'SESSION_USER_INFO';

// Reducers
function initialAppState(){
	return <AppState>{ 
		loading: false,
		userInfo: null 
	}
}

const _createReducer = createReducer(
	initialAppState(), 
	on(loading, s => <AppState>{userInfo: s.userInfo, loading: true}),
	on(loaded, s => <AppState>{userInfo: s.userInfo, loading: false}),
	on(updateUser, (s, user) => <AppState>{userInfo: user, loading: s.loading}),
	on(deleteUser, (s) => <AppState>{userInfo: null, loading: s.loading})
)

export function appStateReducer(state: AppState, action: Action) {
	return _createReducer(state,action);
}


export const selectUserInfo = (state: AppState) => state.userInfo;
export const selectLoading = (state:AppState)  => state.loading;
