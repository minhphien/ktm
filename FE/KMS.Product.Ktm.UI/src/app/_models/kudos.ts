export class Kudos {
    content: string;
    typeName: string;
    senderBadgeId: string;
    senderEmployeeNumber: number;
    senderImgUrl: string;
    senderLastName: string;
    senderFirstMidName: string;
    senderTeam: string;
    receiverBadgeId: string;
    receiverEmployeeNumber: number;
    receiverImgUrl: string;
    receiverLastName: string;
    receiverFirstMidName: string;    
    receiverTeam: string;
    id: number;
    created: Date;
    modified: Date;
}

export class LightKudos {
    ReceiverUsername: string;
    Content: string;
    SlackEmoji: string;
    KudoTypeId: number;
}

export class KudosState {
    kudoReceives: Kudos[];
    kudoSends: Kudos[];
}
