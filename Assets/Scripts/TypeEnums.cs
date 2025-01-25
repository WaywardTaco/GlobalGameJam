
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