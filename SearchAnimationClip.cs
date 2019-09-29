// 호출코드
string animName = ClipIndexToName(anim, data.posIndex);

/// <summary>
/// 애니메이션 클립을 Name으로 반환
/// </summary>
/// <param name="anim">애니메이션 컴포넌트</param>
/// <param name="index">반환할 클립</param>
/// <returns></returns>
string ClipIndexToName(Animation anim, int index)
{
    AnimationClip clip = GetClipByIndex(anim, index);
    if (clip == null)
        return null;
    return clip.name;
}
/// <summary>
/// 애니메이션 클립을 Index로 찾아옴
/// </summary>
/// <param name="anim">애니메이션 컴포넌트</param>
/// <param name="index">찾고 싶은 index값</param>
/// <returns></returns>
AnimationClip GetClipByIndex(Animation anim, int index)
{
    int i = 0;
    foreach (AnimationState animationState in anim)
    {
        if (i == index)
        return animationState.clip;
        i++;
    }
    return null;
}
