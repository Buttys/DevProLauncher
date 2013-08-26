--トラブル・ダイバー
function c12345004.initial_effect(c)
	--special summon
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_SPSUMMON_PROC)
	e1:SetProperty(EFFECT_FLAG_UNCOPYABLE)
	e1:SetRange(LOCATION_HAND)
	e1:SetCondition(c12345004.spcon)
	e1:SetOperation(c12345004.spop)
	c:RegisterEffect(e1)
	--xyzlimit
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_SINGLE)
	e2:SetCode(EFFECT_CANNOT_BE_XYZ_MATERIAL)
	e2:SetProperty(EFFECT_FLAG_CANNOT_DISABLE+EFFECT_FLAG_UNCOPYABLE)
	e2:SetValue(c12345004.xyzlimit)
	c:RegisterEffect(e2)
end
function c12345004.cfilter(c)
	return c:IsFacedown() or c:GetLevel~=4
end
function c12345004.spcon(e,c)
	if c==nil then return true end
	return Duel.GetLocationCount(c:GetControler(),LOCATION_MZONE)>0
		and	Duel.GetFieldGroupCount(c:GetControler(),0,LOCATION_MZONE)>0
		and not Duel.IsExistingMatchingCard(c12345004.cfilter,c:GetControler(),LOCATION_MZONE,0,1,nil)
		and Duel.GetFlagEffect(tp,12345004)==0
end
function c12345004.spop(e,tp)
	Duel.RegisterFlagEffect(tp,12345004,RESET_PHASE+PHASE_END,0,1)
end
function c12345004.xyzlimit(e,c)
	if not c then return false end
	return not c:IsRace(RACE_WARRIOR)
end