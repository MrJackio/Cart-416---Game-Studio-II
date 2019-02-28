// Copyright (c) 2009 David Koontz
// Please direct any bugs/comments/suggestions to david@koontzfamily.org
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using UnityEngine;

public class TextWriterParamMapping {
		
	public static Dictionary<TextWriterVisual.TweenType, Dictionary<string, Type>> mappings = new Dictionary<TextWriterVisual.TweenType, Dictionary<string, Type>>();
	
	static TextWriterParamMapping() {
	
		
		// COLOR FROM
		mappings.Add(TextWriterVisual.TweenType.ColorFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ColorFrom]["color"] = typeof(Color);
		mappings[TextWriterVisual.TweenType.ColorFrom]["r"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["g"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["b"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["a"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["namedcolorvalue"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.ColorFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ColorFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorFrom]["ignoretimescale"] = typeof(bool);
		
		// COLOR TO
		mappings.Add(TextWriterVisual.TweenType.ColorTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ColorTo]["color"] = typeof(Color);
		mappings[TextWriterVisual.TweenType.ColorTo]["r"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["g"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["b"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["a"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["namedcolorvalue"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.ColorTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ColorTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ColorTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ColorTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorTo]["ignoretimescale"] = typeof(bool);
		
		// COLOR UPDATE
		mappings.Add(TextWriterVisual.TweenType.ColorUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ColorUpdate]["color"] = typeof(Color);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["r"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["g"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["b"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["a"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["namedcolorvalue"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.ColorUpdate]["time"] = typeof(float);
		
		// FADE FROM
		mappings.Add(TextWriterVisual.TweenType.FadeFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.FadeFrom]["alpha"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeFrom]["amount"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeFrom]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.FadeFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.FadeFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeFrom]["ignoretimescale"] = typeof(bool);
		
		// FADE TO
		mappings.Add(TextWriterVisual.TweenType.FadeTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.FadeTo]["alpha"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeTo]["amount"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeTo]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.FadeTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.FadeTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.FadeTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.FadeTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.FadeTo]["ignoretimescale"] = typeof(bool);
		
		// FADE UPDATE
		mappings.Add(TextWriterVisual.TweenType.FadeUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.FadeUpdate]["alpha"] = typeof(float);
		mappings[TextWriterVisual.TweenType.FadeUpdate]["includechildren"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.FadeUpdate]["time"] = typeof(float);
		
		// LOOK FROM
		mappings.Add(TextWriterVisual.TweenType.LookFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.LookFrom]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.LookFrom]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookFrom]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.LookFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.LookFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookFrom]["ignoretimescale"] = typeof(bool);
		
		// LOOK TO
		mappings.Add(TextWriterVisual.TweenType.LookTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.LookTo]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.LookTo]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookTo]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.LookTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.LookTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.LookTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.LookTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookTo]["ignoretimescale"] = typeof(bool);
		
		// LOOK UPDATE
		mappings.Add(TextWriterVisual.TweenType.LookUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.LookUpdate]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.LookUpdate]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.LookUpdate]["time"] = typeof(float);
		
		// MOVE ADD
		mappings.Add(TextWriterVisual.TweenType.MoveAdd, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.MoveAdd]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.MoveAdd]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveAdd]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveAdd]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.MoveAdd]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveAdd]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.MoveAdd]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveAdd]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveAdd]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveAdd]["ignoretimescale"] = typeof(bool);

		// MOVE BY
		mappings.Add(TextWriterVisual.TweenType.MoveBy, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.MoveBy]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.MoveBy]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveBy]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveBy]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveBy]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.MoveBy]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.MoveBy]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.MoveBy]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveBy]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveBy]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveBy]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveBy]["ignoretimescale"] = typeof(bool);
		
		// MOVE FROM
		mappings.Add(TextWriterVisual.TweenType.MoveFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.MoveFrom]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveFrom]["path"] = typeof(Vector3OrTransformArray);
		mappings[TextWriterVisual.TweenType.MoveFrom]["movetopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveFrom]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveFrom]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveFrom]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["lookahead"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.MoveFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveFrom]["ignoretimescale"] = typeof(bool);
		
		// MOVE TO
		mappings.Add(TextWriterVisual.TweenType.MoveTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.MoveTo]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveTo]["path"] = typeof(Vector3OrTransformArray);
		mappings[TextWriterVisual.TweenType.MoveTo]["movetopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveTo]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveTo]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveTo]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["lookahead"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.MoveTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.MoveTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.MoveTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveTo]["ignoretimescale"] = typeof(bool);
		
		// MOVE UPDATE
		mappings.Add(TextWriterVisual.TweenType.MoveUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.MoveUpdate]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.MoveUpdate]["time"] = typeof(float);
		
		// PUNCH POSITION
		mappings.Add(TextWriterVisual.TweenType.PunchPosition, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.PunchPosition]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.PunchPosition]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.PunchPosition]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.PunchPosition]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.PunchPosition]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchPosition]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchPosition]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchPosition]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchPosition]["ignoretimescale"] = typeof(bool);

		// PUNCH ROTATION
		mappings.Add(TextWriterVisual.TweenType.PunchRotation, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.PunchRotation]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.PunchRotation]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.PunchRotation]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchRotation]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchRotation]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchRotation]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.PunchRotation]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchRotation]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchRotation]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchRotation]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchRotation]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchRotation]["ignoretimescale"] = typeof(bool);

		// PUNCH SCALE
		mappings.Add(TextWriterVisual.TweenType.PunchScale, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.PunchScale]["position"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.PunchScale]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.PunchScale]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchScale]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchScale]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchScale]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchScale]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.PunchScale]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.PunchScale]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchScale]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchScale]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.PunchScale]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.PunchScale]["ignoretimescale"] = typeof(bool);
		
		// ROTATE ADD
		mappings.Add(TextWriterVisual.TweenType.RotateAdd, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.RotateAdd]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.RotateAdd]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.RotateAdd]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateAdd]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.RotateAdd]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateAdd]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateAdd]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateAdd]["ignoretimescale"] = typeof(bool);
		
		// ROTATE BY
		mappings.Add(TextWriterVisual.TweenType.RotateBy, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.RotateBy]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.RotateBy]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.RotateBy]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateBy]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.RotateBy]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.RotateBy]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateBy]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateBy]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateBy]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateBy]["ignoretimescale"] = typeof(bool);		
		
		// ROTATE FROM
		mappings.Add(TextWriterVisual.TweenType.RotateFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.RotateFrom]["rotation"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.RotateFrom]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.RotateFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.RotateFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateFrom]["ignoretimescale"] = typeof(bool);		
		
		// ROTATE TO
		mappings.Add(TextWriterVisual.TweenType.RotateTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.RotateTo]["rotation"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.RotateTo]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.RotateTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.RotateTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.RotateTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.RotateTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.RotateTo]["ignoretimescale"] = typeof(bool);			
		
		// ROTATE UPDATE
		mappings.Add(TextWriterVisual.TweenType.RotateUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.RotateUpdate]["rotation"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.RotateUpdate]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateUpdate]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateUpdate]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.RotateUpdate]["islocal"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.RotateUpdate]["time"] = typeof(float);
		
		// SCALE ADD
		mappings.Add(TextWriterVisual.TweenType.ScaleAdd, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ScaleAdd]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleAdd]["ignoretimescale"] = typeof(bool);
		
		// SCALE BY
		mappings.Add(TextWriterVisual.TweenType.ScaleBy, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ScaleBy]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.ScaleBy]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleBy]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ScaleBy]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleBy]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleBy]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleBy]["ignoretimescale"] = typeof(bool);
		
		// SCALE FROM
		mappings.Add(TextWriterVisual.TweenType.ScaleFrom, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ScaleFrom]["scale"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleFrom]["ignoretimescale"] = typeof(bool);
		
		// SCALE TO
		mappings.Add(TextWriterVisual.TweenType.ScaleTo, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ScaleTo]["scale"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.ScaleTo]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["speed"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleTo]["easetype"] = typeof(iTween.EaseType);
		mappings[TextWriterVisual.TweenType.ScaleTo]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleTo]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ScaleTo]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ScaleTo]["ignoretimescale"] = typeof(bool);
		
		// SCALE UPDATE
		mappings.Add(TextWriterVisual.TweenType.ScaleUpdate, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ScaleUpdate]["scale"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.ScaleUpdate]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleUpdate]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleUpdate]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ScaleUpdate]["time"] = typeof(float);
		
		// SHAKE POSITION
		mappings.Add(TextWriterVisual.TweenType.ShakePosition, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ShakePosition]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.ShakePosition]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.ShakePosition]["orienttopath"] = typeof(bool);
		mappings[TextWriterVisual.TweenType.ShakePosition]["looktarget"] = typeof(Vector3OrTransform);
		mappings[TextWriterVisual.TweenType.ShakePosition]["looktime"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["axis"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakePosition]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakePosition]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakePosition]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakePosition]["ignoretimescale"] = typeof(bool);
		
		// SHAKE ROTATION
		mappings.Add(TextWriterVisual.TweenType.ShakeRotation, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ShakeRotation]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["space"] = typeof(Space);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeRotation]["ignoretimescale"] = typeof(bool);
		
		// SHAKE SCALE
		mappings.Add(TextWriterVisual.TweenType.ShakeScale, new Dictionary<string, Type>());
		mappings[TextWriterVisual.TweenType.ShakeScale]["amount"] = typeof(Vector3);
		mappings[TextWriterVisual.TweenType.ShakeScale]["x"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeScale]["y"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeScale]["z"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeScale]["time"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeScale]["delay"] = typeof(float);
		mappings[TextWriterVisual.TweenType.ShakeScale]["looptype"] = typeof(iTween.LoopType);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onstart"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onstarttarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onstartparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onupdate"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onupdatetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeScale]["onupdateparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["oncomplete"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["oncompletetarget"] = typeof(GameObject);
		mappings[TextWriterVisual.TweenType.ShakeScale]["oncompleteparams"] = typeof(string);
		mappings[TextWriterVisual.TweenType.ShakeScale]["ignoretimescale"] = typeof(bool);
		
			
	}
}