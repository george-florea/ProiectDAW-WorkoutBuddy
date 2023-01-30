import { IEditInfo } from "../editProfile/IEditInfo";
import { IUserProfile } from "../userProfile/IUserProfile";


export interface IAccountState {
    userProfile: IUserProfile,
    editInfo: IEditInfo
}

export const initialState = {
    userProfile: {},
    editInfo: {}
} as IAccountState;
