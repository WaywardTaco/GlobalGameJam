
namespace Enums {
    public enum WorldEventType {
        None, 
        LMEProduct, PPHProduct, BIDProduct, OBGProduct, SFIProduct, LDDProduct,
        LMECancelled, PPHCancelled, BIDCancelled, OBGCancelled, SFICancelled, LDDCancelled,
        LMEShortage, PPHShortage, BIDShortage, OBGShortage, SFIShortage, LDDShortage,
        LMEEmerge, PPHEmerge, BIDEmerge, OBGEmerge, SFIEmerge, LDDEmerge,
        LMEUseless, PPHUseless, BIDUseless, OBGUseless, SFIUseless, LDDUseless,
        LMEMeme, PPHMeme, BIDMeme, OBGMeme, SFIMeme, LDDMeme,
        LMEGovAllow, PPHGovAllow, BIDGovAllow, OBGGovAllow, SFIGovAllow, LDDGovAllow,
        LMEGovBan, PPHGovBan, BIDGovBan, OBGGovBan, SFIGovBan, LDDGovBan,
        LMEVirus, PPHVirus, BIDVirus, OBGVirus, SFIVirus, LDDVirus,
<<<<<<< HEAD
        PolInv0, PolInv1, PolInv2, PolInv3, PolInv4, PolInv5 , CSR, Tax, 
        Charity, Diversity, Lobbying, Insider, Pyramid, Blackmail, Shell, 
        Downpayment, Hacker, Bond, PR, Bitcoin, Narrative
=======
        PolInv0, PolInv1, PolInv2, PolInv3, PolInv4, PolInv5, PolInv6,
>>>>>>> 10b09c1fb92156602dcc4c34dee6a401d35e2d89
    }
    
    public enum UpgradeType {
        None,
        PR,
        Lobby,
        Bitcoin,
        Bond,
        CSR,
        Tax,
        Charity,
        Diversity,
        Media,
        Insider,
        Pyramid,
        Blackmail,
        Shell,
        Downpayment,
        Hacker
    }
    
    public enum ResourceType {
        None,
    }

    public enum StockType {
        None, 
        LME, PPH, BID, OBK, SFI, LDD
    }

}