#import <AVFoundation/AVFoundation.h>
extern "C" void _SpeakIOS(const char* message, const char* lang)
{
    if (!message) return;
    @autoreleasepool {
        NSString* text = [NSString stringWithUTF8String:message];
        NSString* langCode = lang ? [NSString stringWithUTF8String:lang] : @"en-US";
        AVSpeechUtterance* utter = [AVSpeechUtterance speechUtteranceWithString:text];
        utter.voice = [AVSpeechSynthesisVoice voiceWithLanguage:langCode];
        utter.rate = AVSpeechUtteranceDefaultSpeechRate;
        [[AVAudioSession sharedInstance] setCategory:AVAudioSessionCategoryPlayback withOptions:0 error:nil];
        static AVSpeechSynthesizer* synth;
        if (!synth) synth = [AVSpeechSynthesizer new];
        [synth speakUtterance:utter];
    }
}