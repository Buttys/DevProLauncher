--Angel of Zera
function c80500087.initial_effect(c)
	--synchro summon
	aux.AddSynchroProcedure(c,nil,aux.NonTuner(nil),1)
	c:EnableReviveLimit()
	--Atk up
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_SINGLE)
	e1:SetCode(EFFECT_UPDATE_ATTACK)
	e1:SetProperty(EFFECT_FLAG_SINGLE_RANGE)
	e1:SetRange(LOCATION_MZONE)
	e1:SetValue(c80500087.atkval)
	c:RegisterEffect(e1)
	--Banished
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_CONTINUOUS)
	e2:SetCode(EVENT_REMOVE)
	e2:SetOperation(c80500087.regop)
	c:RegisterEffect(e2)
end
function c80500087.atkval(e,c)
	return Duel.GetFieldGroupCount(c:GetControler(),0,LOCATION_REMOVED)*100
end
function c80500087.regop(e,tp,eg,ep,ev,re,r,rp)
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
	e1:SetCode(EVENT_PHASE+PHASE_STANDBY)
	e1:SetCountLimit(1)
	e1:SetLabel(Duel.GetTurnCount())
	e1:SetCondition(c80500087.spcon1)
	e1:SetOperation(c80500087.spop1)
	e1:SetReset(RESET_PHASE+PHASE_END,2)
	Duel.RegisterEffect(e1,tp)
end
function c80500087.spcon1(e,tp,eg,ep,ev,re,r,rp)
	return Duel.GetTurnCount()~=e:GetLabel()
end
function c80500087.spcon2(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():GetLocation()==LOCATION_REMOVED
end
function c80500087.spop1(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if Duel.GetLocationCount(tp,LOCATION_MZONE)>0
		and c:IsCanBeSpecialSummoned(e,0,tp,false,false) then
		local e1=Effect.CreateEffect(c)
		e1:SetDescription(aux.Stringid(80500087,2))
		e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
		e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
		e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_F)
		e1:SetCode(EVENT_PHASE+PHASE_STANDBY)
		e1:SetCountLimit(1)
		e1:SetCost(c80500087.spcost)
		e1:SetCondition(c80500087.spcon2)
		e1:SetOperation(c80500087.spop2)
		e1:SetReset(RESET_PHASE+PHASE_END)
		Duel.RegisterEffect(e1,tp)
	end
end
function c80500087.spop2(e,tp,eg,ep,ev,re,r,rp)
	local tc=e:GetHandler()
	if tc then
		Duel.SpecialSummon(tc,0,tp,tp,false,false,POS_FACEUP)
	end
end
function c80500087.spcost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,80500087)==0 end
	Duel.RegisterFlagEffect(tp,80500087,RESET_PHASE+PHASE_END,0,1)
end