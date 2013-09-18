--サイレントアングラー
function c80100002.initial_effect(c)
	--special summon
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_SPSUMMON_PROC)
	e1:SetProperty(EFFECT_FLAG_UNCOPYABLE)
	e1:SetRange(LOCATION_HAND)
	e1:SetCondition(c80100002.spcon)
	e1:SetOperation(c80100002.spop)
	c:RegisterEffect(e1)
end
function c80100002.filter(c)
	return c:IsFaceup() and c:IsAttribute(ATTRIBUTE_WATER)
end
function c80100002.spcon(e,c)
	if c==nil then return true end
	local tp=c:GetControler()
	return 	Duel.GetLocationCount(tp,LOCATION_MZONE)>0
		and Duel.IsExistingMatchingCard(c80100002.filter,tp,LOCATION_MZONE,0,1,nil)
end
function c80100002.spop(e,tp,eg,ep,ev,re,r,rp,c)
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e1:SetCode(EFFECT_CANNOT_SPECIAL_SUMMON)
	e1:SetReset(RESET_PHASE+PHASE_END)
	e1:SetTargetRange(1,0)
	e1:SetTarget(c80100002.sumlimit)
	Duel.RegisterEffect(e1,tp)
end
function c80100002.sumlimit(e,c)
	return c:IsLocation(LOCATION_HAND)
end
