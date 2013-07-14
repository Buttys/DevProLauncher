--先史遺産アステカ・マスク・ゴーレム
function c94766498.initial_effect(c)
	c:SetUniqueOnField(1,0,94766498)
	--special summon
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
	e1:SetCode(EVENT_CHAINING)
	e1:SetRange(LOCATION_HAND)
	e1:SetOperation(c94766498.chop)
	c:RegisterEffect(e1)
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_FIELD)
	e2:SetCode(EFFECT_SPSUMMON_PROC)
	e2:SetProperty(EFFECT_FLAG_UNCOPYABLE)
	e2:SetRange(LOCATION_HAND)
	e2:SetCondition(c94766498.spcon)
	c:RegisterEffect(e2)
end
function c94766498.chop(e,tp,eg,ep,ev,re,r,rp)
	if re:GetHandler():IsType(TYPE_SPELL) and re:IsHasType(EFFECT_TYPE_ACTIVATE) and re:GetHandler():IsSetCard(0x70) 
	and rp==tp then
		e:GetHandler():RegisterFlagEffect(94766498,RESET_EVENT+0x2fe0000+RESET_PHASE+PHASE_END,0,1)
	end
end
function c94766498.spcon(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return c:GetFlagEffect(94766498)~=0
end