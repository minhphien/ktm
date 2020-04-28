export class Kudos {
    content: string;
    typeName: string;
    senderBadgeId: string;
    senderLastName: string;
    senderFirstMidName: string;
    senderTeam: string;
    receiverBadgeId: string;
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
