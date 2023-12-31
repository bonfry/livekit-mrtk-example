// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: livekit_webhook.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace LiveKit.Proto {

  /// <summary>Holder for reflection information generated from livekit_webhook.proto</summary>
  public static partial class LivekitWebhookReflection {

    #region Descriptor
    /// <summary>File descriptor for livekit_webhook.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LivekitWebhookReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVsaXZla2l0X3dlYmhvb2sucHJvdG8SB2xpdmVraXQaFGxpdmVraXRfbW9k",
            "ZWxzLnByb3RvGhRsaXZla2l0X2VncmVzcy5wcm90bxoVbGl2ZWtpdF9pbmdy",
            "ZXNzLnByb3RvIoICCgxXZWJob29rRXZlbnQSDQoFZXZlbnQYASABKAkSGwoE",
            "cm9vbRgCIAEoCzINLmxpdmVraXQuUm9vbRItCgtwYXJ0aWNpcGFudBgDIAEo",
            "CzIYLmxpdmVraXQuUGFydGljaXBhbnRJbmZvEigKC2VncmVzc19pbmZvGAkg",
            "ASgLMhMubGl2ZWtpdC5FZ3Jlc3NJbmZvEioKDGluZ3Jlc3NfaW5mbxgKIAEo",
            "CzIULmxpdmVraXQuSW5ncmVzc0luZm8SIQoFdHJhY2sYCCABKAsyEi5saXZl",
            "a2l0LlRyYWNrSW5mbxIKCgJpZBgGIAEoCRISCgpjcmVhdGVkX2F0GAcgASgD",
            "QkZaI2dpdGh1Yi5jb20vbGl2ZWtpdC9wcm90b2NvbC9saXZla2l0qgINTGl2",
            "ZUtpdC5Qcm90b+oCDkxpdmVLaXQ6OlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::LiveKit.Proto.LivekitModelsReflection.Descriptor, global::LiveKit.Proto.LivekitEgressReflection.Descriptor, global::LiveKit.Proto.LivekitIngressReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::LiveKit.Proto.WebhookEvent), global::LiveKit.Proto.WebhookEvent.Parser, new[]{ "Event", "Room", "Participant", "EgressInfo", "IngressInfo", "Track", "Id", "CreatedAt" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class WebhookEvent : pb::IMessage<WebhookEvent>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<WebhookEvent> _parser = new pb::MessageParser<WebhookEvent>(() => new WebhookEvent());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<WebhookEvent> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::LiveKit.Proto.LivekitWebhookReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WebhookEvent() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WebhookEvent(WebhookEvent other) : this() {
      event_ = other.event_;
      room_ = other.room_ != null ? other.room_.Clone() : null;
      participant_ = other.participant_ != null ? other.participant_.Clone() : null;
      egressInfo_ = other.egressInfo_ != null ? other.egressInfo_.Clone() : null;
      ingressInfo_ = other.ingressInfo_ != null ? other.ingressInfo_.Clone() : null;
      track_ = other.track_ != null ? other.track_.Clone() : null;
      id_ = other.id_;
      createdAt_ = other.createdAt_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WebhookEvent Clone() {
      return new WebhookEvent(this);
    }

    /// <summary>Field number for the "event" field.</summary>
    public const int EventFieldNumber = 1;
    private string event_ = "";
    /// <summary>
    /// one of room_started, room_finished, participant_joined, participant_left,
    /// track_published, track_unpublished, egress_started, egress_updated, egress_ended, ingress_started, ingress_ended
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Event {
      get { return event_; }
      set {
        event_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "room" field.</summary>
    public const int RoomFieldNumber = 2;
    private global::LiveKit.Proto.Room room_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::LiveKit.Proto.Room Room {
      get { return room_; }
      set {
        room_ = value;
      }
    }

    /// <summary>Field number for the "participant" field.</summary>
    public const int ParticipantFieldNumber = 3;
    private global::LiveKit.Proto.ParticipantInfo participant_;
    /// <summary>
    /// set when event is participant_* or track_*
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::LiveKit.Proto.ParticipantInfo Participant {
      get { return participant_; }
      set {
        participant_ = value;
      }
    }

    /// <summary>Field number for the "egress_info" field.</summary>
    public const int EgressInfoFieldNumber = 9;
    private global::LiveKit.Proto.EgressInfo egressInfo_;
    /// <summary>
    /// set when event is egress_*
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::LiveKit.Proto.EgressInfo EgressInfo {
      get { return egressInfo_; }
      set {
        egressInfo_ = value;
      }
    }

    /// <summary>Field number for the "ingress_info" field.</summary>
    public const int IngressInfoFieldNumber = 10;
    private global::LiveKit.Proto.IngressInfo ingressInfo_;
    /// <summary>
    /// set when event is ingress_*
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::LiveKit.Proto.IngressInfo IngressInfo {
      get { return ingressInfo_; }
      set {
        ingressInfo_ = value;
      }
    }

    /// <summary>Field number for the "track" field.</summary>
    public const int TrackFieldNumber = 8;
    private global::LiveKit.Proto.TrackInfo track_;
    /// <summary>
    /// set when event is track_*
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::LiveKit.Proto.TrackInfo Track {
      get { return track_; }
      set {
        track_ = value;
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 6;
    private string id_ = "";
    /// <summary>
    /// unique event uuid
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Id {
      get { return id_; }
      set {
        id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "created_at" field.</summary>
    public const int CreatedAtFieldNumber = 7;
    private long createdAt_;
    /// <summary>
    /// timestamp in seconds
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long CreatedAt {
      get { return createdAt_; }
      set {
        createdAt_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as WebhookEvent);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(WebhookEvent other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Event != other.Event) return false;
      if (!object.Equals(Room, other.Room)) return false;
      if (!object.Equals(Participant, other.Participant)) return false;
      if (!object.Equals(EgressInfo, other.EgressInfo)) return false;
      if (!object.Equals(IngressInfo, other.IngressInfo)) return false;
      if (!object.Equals(Track, other.Track)) return false;
      if (Id != other.Id) return false;
      if (CreatedAt != other.CreatedAt) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Event.Length != 0) hash ^= Event.GetHashCode();
      if (room_ != null) hash ^= Room.GetHashCode();
      if (participant_ != null) hash ^= Participant.GetHashCode();
      if (egressInfo_ != null) hash ^= EgressInfo.GetHashCode();
      if (ingressInfo_ != null) hash ^= IngressInfo.GetHashCode();
      if (track_ != null) hash ^= Track.GetHashCode();
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (CreatedAt != 0L) hash ^= CreatedAt.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Event.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Event);
      }
      if (room_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Room);
      }
      if (participant_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Participant);
      }
      if (Id.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Id);
      }
      if (CreatedAt != 0L) {
        output.WriteRawTag(56);
        output.WriteInt64(CreatedAt);
      }
      if (track_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(Track);
      }
      if (egressInfo_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(EgressInfo);
      }
      if (ingressInfo_ != null) {
        output.WriteRawTag(82);
        output.WriteMessage(IngressInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Event.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Event);
      }
      if (room_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Room);
      }
      if (participant_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Participant);
      }
      if (Id.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Id);
      }
      if (CreatedAt != 0L) {
        output.WriteRawTag(56);
        output.WriteInt64(CreatedAt);
      }
      if (track_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(Track);
      }
      if (egressInfo_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(EgressInfo);
      }
      if (ingressInfo_ != null) {
        output.WriteRawTag(82);
        output.WriteMessage(IngressInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Event.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Event);
      }
      if (room_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Room);
      }
      if (participant_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Participant);
      }
      if (egressInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(EgressInfo);
      }
      if (ingressInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(IngressInfo);
      }
      if (track_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Track);
      }
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (CreatedAt != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(CreatedAt);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(WebhookEvent other) {
      if (other == null) {
        return;
      }
      if (other.Event.Length != 0) {
        Event = other.Event;
      }
      if (other.room_ != null) {
        if (room_ == null) {
          Room = new global::LiveKit.Proto.Room();
        }
        Room.MergeFrom(other.Room);
      }
      if (other.participant_ != null) {
        if (participant_ == null) {
          Participant = new global::LiveKit.Proto.ParticipantInfo();
        }
        Participant.MergeFrom(other.Participant);
      }
      if (other.egressInfo_ != null) {
        if (egressInfo_ == null) {
          EgressInfo = new global::LiveKit.Proto.EgressInfo();
        }
        EgressInfo.MergeFrom(other.EgressInfo);
      }
      if (other.ingressInfo_ != null) {
        if (ingressInfo_ == null) {
          IngressInfo = new global::LiveKit.Proto.IngressInfo();
        }
        IngressInfo.MergeFrom(other.IngressInfo);
      }
      if (other.track_ != null) {
        if (track_ == null) {
          Track = new global::LiveKit.Proto.TrackInfo();
        }
        Track.MergeFrom(other.Track);
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.CreatedAt != 0L) {
        CreatedAt = other.CreatedAt;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Event = input.ReadString();
            break;
          }
          case 18: {
            if (room_ == null) {
              Room = new global::LiveKit.Proto.Room();
            }
            input.ReadMessage(Room);
            break;
          }
          case 26: {
            if (participant_ == null) {
              Participant = new global::LiveKit.Proto.ParticipantInfo();
            }
            input.ReadMessage(Participant);
            break;
          }
          case 50: {
            Id = input.ReadString();
            break;
          }
          case 56: {
            CreatedAt = input.ReadInt64();
            break;
          }
          case 66: {
            if (track_ == null) {
              Track = new global::LiveKit.Proto.TrackInfo();
            }
            input.ReadMessage(Track);
            break;
          }
          case 74: {
            if (egressInfo_ == null) {
              EgressInfo = new global::LiveKit.Proto.EgressInfo();
            }
            input.ReadMessage(EgressInfo);
            break;
          }
          case 82: {
            if (ingressInfo_ == null) {
              IngressInfo = new global::LiveKit.Proto.IngressInfo();
            }
            input.ReadMessage(IngressInfo);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Event = input.ReadString();
            break;
          }
          case 18: {
            if (room_ == null) {
              Room = new global::LiveKit.Proto.Room();
            }
            input.ReadMessage(Room);
            break;
          }
          case 26: {
            if (participant_ == null) {
              Participant = new global::LiveKit.Proto.ParticipantInfo();
            }
            input.ReadMessage(Participant);
            break;
          }
          case 50: {
            Id = input.ReadString();
            break;
          }
          case 56: {
            CreatedAt = input.ReadInt64();
            break;
          }
          case 66: {
            if (track_ == null) {
              Track = new global::LiveKit.Proto.TrackInfo();
            }
            input.ReadMessage(Track);
            break;
          }
          case 74: {
            if (egressInfo_ == null) {
              EgressInfo = new global::LiveKit.Proto.EgressInfo();
            }
            input.ReadMessage(EgressInfo);
            break;
          }
          case 82: {
            if (ingressInfo_ == null) {
              IngressInfo = new global::LiveKit.Proto.IngressInfo();
            }
            input.ReadMessage(IngressInfo);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
